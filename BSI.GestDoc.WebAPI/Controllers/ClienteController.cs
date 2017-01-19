using System;
using System.Web.Http;
using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.CRUD;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{

    [System.Web.Http.RoutePrefix("api/Cliente")]
    public class ClienteController : ApiController
    {
        /// <summary>
        /// Consulta o cliente na base de dados pelo id
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns>Cliente</returns>
        [Authorize]
        [System.Web.Http.Route("Consultar")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Consultar(long clienteId)
        {

            Cliente ClienteRetorno = null;

            try
            {
                ClienteRetorno = new ClienteDal().GetCliente(clienteId);

                return Ok(ClienteRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
            finally
            {
                this.Dispose();
            }

        }
    }

}
