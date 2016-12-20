using System;
using System.Collections.Generic;
using BSI.GestDoc.Entity;
using System.Web.Http;
using BSI.GestDoc.Repository.DAL;

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
            IEnumerable<DocumentoClienteDados> documentosCliente = null;

            try
            {
                documentosCliente = Dal.ListarPropostas(usuarioId, clientId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

            return Ok(documentosCliente);
        }

    }

}
