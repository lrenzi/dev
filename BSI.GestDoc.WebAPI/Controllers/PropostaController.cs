using System;
using System.Collections.Generic;
using BSI.GestDoc.Entity;
using System.Web.Http;
using BSI.GestDoc.Repository.DAL;
using BSI.GestDoc.BusinessLogic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Proposta")]
    public class PropostaController : ApiController
    {

        [System.Web.Http.Route("ListarPropostas")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult ListarPropostas(string usuarioId, string clientId, string numeroProposta)
        {

            PropostasDal Dal = new PropostasDal();
            DocumentoClienteBL documentoClienteBL = new DocumentoClienteBL();
            List<DocumentoClienteTipo> listaPropostas = null;

            try
            {
                listaPropostas = documentoClienteBL.ListarDocumentosCliente(usuarioId, clientId, numeroProposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

            return Ok(listaPropostas);
        }

    }

}
