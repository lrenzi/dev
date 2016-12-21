using System;
using System.Collections.Generic;
using BSI.GestDoc.Entity;
using System.Web.Http;
using BSI.GestDoc.Repository.DAL;
using BSI.GestDoc.BusinessLogic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Propostas")]
    public class PropostasController : ApiController
    {

        [System.Web.Http.Route("ListarPropostas")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult ListarPropostas(string usuarioId, string clientId)
        {

            PropostasDal Dal = new PropostasDal();
            List<DocumentoClienteDados> listaPropostas = null;
            DocumentoClienteBL documentoClienteBL = new DocumentoClienteBL();

            try
            {
                listaPropostas = documentoClienteBL.ListarDocumentosCliente(usuarioId, clientId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

            return Ok(listaPropostas);
        }

    }

}
