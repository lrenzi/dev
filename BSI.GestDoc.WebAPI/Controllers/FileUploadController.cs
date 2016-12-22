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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/FileUpload")]
    public class FileUploadController : ApiController
    {
        //public HttpResponseMessage Get()
        //{
        //    var body = RenderViewToString("FileUpload", "~/Views/FileUpload/Index.cshtml", new object());
        //    return Request.CreateResponse(HttpStatusCode.OK, new { content = body });
        //}

        //public static string RenderViewToString(string controllerName, string viewName, object viewData)
        //{
        //    using (var writer = new StringWriter())
        //    {
        //        var routeData = new RouteData();
        //        routeData.Values.Add("controller", controllerName);
        //        var fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, new FakeController());
        //        var razorViewEngine = new RazorViewEngine();
        //        var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);

        //        var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer);
        //        razorViewResult.View.Render(viewContext, writer);
        //        return writer.ToString();
        //    }
        //}

        //[System.Web.Http.Route("EnviarArquivos")]
        //[System.Web.Http.HttpPost]
        //[ValidateMimeMultipartContentFilter]
        //public async Task<FileResult> EnviarArquivos(dynamic parametro)
        //{
        //    UploadFile upload = new WebAPI.UploadFile();
        //    return await new UploadFile().GetFile(Request);
        //}

        [System.Web.Http.Route("RetornarArquivo")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage RetornarArquivo([FromBody]DocumentoCliente documentoCliente)
        {
            DocumentoCliente _documentoCliente = new UploadFileBL().RetornarArquivo(documentoCliente);
            UploadFileBL uploadFileBl = new UploadFileBL();

            var file = WorkingFolder + "\\" + _documentoCliente.DocClienteNomeArquivoSalvo;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(file, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = _documentoCliente.DocClienteNomeArquivoOriginal
            };
            return result;
        }

        private readonly string WorkingFolder = HttpRuntime.AppDomainAppPath + ConfigurationManager.AppSettings["DiretorioUpload"];

        [System.Web.Http.Route("EnviarArquivos")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> EnviarArquivos(int usuarioId, int clienteId, int docCliTipoId, bool reenvio)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            DocumentoCliente _documentoCliente = new DocumentoCliente();

            try
            {
                var streamProvider = new MultipartFormDataStreamProvider(WorkingFolder);
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                _documentoCliente = new DocumentoCliente()
                {
                    UsuarioId = usuarioId,
                    ClienteId = clienteId,
                    DocCliTipoId = docCliTipoId,
                    DocClienteNomeArquivoSalvo = streamProvider.FileData.Select(entry => entry.LocalFileName).First().Split(char.Parse("\\")).Last(),
                    DocClienteNomeArquivoOriginal = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName).First().Replace("\"", ""),
                    DocClienteTipoArquivo = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType).First(),
                    DocClienteDataUpload = DateTime.Now
                };

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
                
                return Ok((new Retorno() { Dados = _documentoCliente,  Mensagem  = "Arquivo incluído com sucesso." }));
            }
            catch (BusinessLogic.BusinessException.BusinessException ex)
            {
                if (File.Exists(WorkingFolder + "\\" + _documentoCliente.DocClienteNomeArquivoSalvo))
                    File.Delete(WorkingFolder + "\\" + _documentoCliente.DocClienteNomeArquivoSalvo);

                return Ok(ex.GetRetorno());
            }
            catch (Exception ex)
            {
                if (File.Exists(WorkingFolder + "\\" + _documentoCliente.DocClienteNomeArquivoSalvo))
                    File.Delete(WorkingFolder + "\\" + _documentoCliente.DocClienteNomeArquivoSalvo);
              
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        /*
        // Extracts Request FormatData as a strongly typed model
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData
                    .GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
            }

            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
        
        [System.Web.Http.Route("EnviarArquivos2")]
        [System.Web.Http.HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<FileResult> EnviarArquivos2()
        {
            UploadFile upload = new WebAPI.UploadFile();
            return await new UploadFile().GetFile(Request);
        }
        */

        [System.Web.Http.Route("RetornarDocumentoClienteTipo")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult RetornarDocumentoClienteTipo([FromBody]DocumentoClienteTipo documentoClienteTipo)
        {
            var DocumentosClienteTipo = new BusinessLogic.UploadFileBL().RetornarDocumentoClienteTipo(documentoClienteTipo.ClienteId);
            return Ok(DocumentosClienteTipo);
        }


    }
    //public class FakeController : ControllerBase { protected override void ExecuteCore() { } }
}
