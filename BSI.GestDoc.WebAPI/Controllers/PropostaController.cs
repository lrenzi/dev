using System;
using System.Collections.Generic;
using BSI.GestDoc.Entity;
using System.Web.Http;
using BSI.GestDoc.Repository.DAL;
using BSI.GestDoc.BusinessLogic;
using BSI.GestDoc.Repository.CRUD;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Proposta")]
    public class PropostaController : ApiController
    {
        /// <summary>
        /// Recupera detalhes da proposta pesquisada
        /// </summary>        
        /// <returns>List<DocumentoClienteTipo></returns>
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("ConsultaProposta")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult ConsultaProposta(Int16 usuarioId, Int16 clientId,string numeroProposta)
        {
            List<DocumentoClienteTipo> listaPropostas = null;

            try
            {
                listaPropostas = new DocumentoClienteBL().ListarDocumentosCliente(usuarioId.ToString(), clientId.ToString(), numeroProposta);             
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
            finally
            {
                this.Dispose();
            }

            return Ok(listaPropostas);
        }

        /// <summary>
        /// Recupera lista de Propostas
        /// </summary>        
        /// <returns>List<DocumentoClienteTipo></returns>
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("ListarPropostas")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult ListarPropostas(Int16 usuarioId, Int16 clientId)
        {
            IEnumerable<DocumentoClienteDados> documentosCliente = null;

            try
            {
                
                documentosCliente = new DocumentoClienteDadosDal().GetAllByUsuarioIdClienteId(clientId, usuarioId);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
            finally
            {
                this.Dispose();
            }

            return Ok(documentosCliente);
        }

    }

}
