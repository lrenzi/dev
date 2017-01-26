using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.GestDoc.Entity;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using Newtonsoft.Json;
using BSI.GestDoc.BusinessLogic;
using System.Configuration;
using BSI.GestDoc.BusinessLogic.Util;
using System.Net.Http.Headers;
using BSI.GestDoc.Util;
using BSI.GestDoc.CustomException.BusinessException;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/FileUpload")]
    public class FileUploadController : ApiController
    {
        private string WorkingFolder = HttpRuntime.AppDomainAppPath + ConfigurationManager.AppSettings["DiretorioUpload"];

        [System.Web.Http.Authorize]
        [System.Web.Http.Route("RetornarArquivo")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage RetornarArquivo(string docClienteId)
        {
            docClienteId = MD5Crypt.Descriptografar(docClienteId);

            DocumentoCliente _documentoCliente = new UploadFileBL().RetornarArquivo(new DocumentoCliente() { DocClienteId = long.Parse(docClienteId) });
            UploadFileBL uploadFileBl = new UploadFileBL();

            WorkingFolder += "\\" + new UploadFileBL().RecuperarCaminhoPastaDocumentosByDocClienteId(long.Parse(docClienteId));

            var file = WorkingFolder + "\\" + _documentoCliente.DocClienteNomeArquivoSalvo;
            HttpResponseMessage result = null;
            if (!File.Exists(file))
            {
                result = new HttpResponseMessage(HttpStatusCode.Conflict);
                return result;
            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(file, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = _documentoCliente.DocClienteNomeArquivoOriginal
                };
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                return result;
            }

        }

        [System.Web.Http.Authorize]
        [System.Web.Http.Route("EnviarArquivos")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> EnviarArquivos(int usuarioId, long clienteId, int docCliTipoId, bool reenvio)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            DocumentoCliente _documentoCliente = new DocumentoCliente();

            try
            {
                string directory = new UploadFileBL().RecuperarCaminhoPastaDocumentosByClienteId(clienteId);

                WorkingFolder += "\\" + directory;

                if (!Directory.Exists(WorkingFolder))
                {
                    Directory.CreateDirectory(WorkingFolder);
                }

                var streamProvider = new MultipartFormDataStreamProvider(WorkingFolder);
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                _documentoCliente = new DocumentoCliente()
                {
                    UsuarioId = usuarioId,
                    ClienteId = clienteId,
                    DocCliTipoId = docCliTipoId,
                    DocClienteCaminhoCompletoArquivoSalvo = streamProvider.FileData.Select(entry => entry.LocalFileName).First(),
                    DocClienteNomeArquivoSalvo = System.IO.Path.GetFileName(streamProvider.FileData.Select(entry => entry.LocalFileName).First()),
                    DocClienteNomeArquivoOriginal = System.IO.Path.GetFileName(streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName).First().Replace("\"", "")),
                    DocClienteTipoArquivo = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType).First(),
                    DocClienteDataUpload = DateTime.Now
                };

                #region 1 - Verifica tipo/ versão pdf
                if (UtilFile.GetMIMEType(_documentoCliente.DocClienteNomeArquivoOriginal).ToLower() != "application/pdf")
                {
                    throw new Exception("Erro - Documento deve ser do tipo PDF");
                }
                #endregion

                Cliente cliente = new Cliente() { ClienteId = _documentoCliente.ClienteId };
                UploadFileBL uploadFileBL = null;

                switch (cliente.ClienteNomeEnum)
                {
                    case Cliente.EnumCliente.Bradesco:
                        uploadFileBL = new UploadFileBradescoBL();
                        ((UploadFileBradescoBL)uploadFileBL).Reenvio = reenvio;
                        ((UploadFileBradescoBL)uploadFileBL).WorkingFolder = WorkingFolder;
                        _documentoCliente = uploadFileBL.EnviarDocumentoCliente(_documentoCliente);
                        break;

                    default:
                        uploadFileBL = new UploadFileBL();
                        _documentoCliente = uploadFileBL.EnviarDocumentoCliente(_documentoCliente);
                        break;
                }

                return Ok((new Retorno() { Dados = _documentoCliente, Mensagem = "Arquivo incluído com sucesso.", TipoMensagem = EnumTipoMensagem.Sucesso }));
            }
            catch (BusinessException ex)
            {
                if (File.Exists(WorkingFolder + "\\" + _documentoCliente.DocClienteNomeArquivoSalvo))
                    File.Delete(WorkingFolder + "\\" + _documentoCliente.DocClienteNomeArquivoSalvo);

                return Ok(ex.GetRetorno());
            }
            catch (Exception ex)
            {
                if (File.Exists(WorkingFolder + "\\" + _documentoCliente.DocClienteNomeArquivoSalvo))
                    File.Delete(WorkingFolder + "\\" + _documentoCliente.DocClienteNomeArquivoSalvo);

                throw new CustomException.CustomException(ex.Message, ex);
            }
        }


        [System.Web.Http.Authorize]
        [System.Web.Http.Route("RetornarDocumentoClienteTipo")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult RetornarDocumentoClienteTipo([FromBody]DocumentoClienteTipo documentoClienteTipo)
        {
            List<DocumentoClienteTipo> retorno = null;
            retorno = new BusinessLogic.UploadFileBL().RetornarDocumentoClienteTipo(documentoClienteTipo.ClienteId);
            return Ok(retorno);
        }
    }
}
