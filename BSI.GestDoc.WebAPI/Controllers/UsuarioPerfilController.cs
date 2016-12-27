using System;
using BSI.GestDoc.Entity;
using System.Web.Http;
using BSI.GestDoc.BusinessLogic;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/UsuarioPerfil")]
    public class UsuarioPerfilController : ApiController
    {
        /// <summary>
        /// Consulta os perfis de usuarios cadastrados na base de dados
        /// </summary>
        /// <param name="usuPerfilId"></param>
        /// <param name="clienteId"></param>
        /// <param name="usuPerfilNome"></param>
        /// <param name="usuPerfilDescricao"></param>
        /// <returns></returns>
        [System.Web.Http.Route("Consultar")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Consultar(string usuPerfilId, string clienteId, string usuPerfilNome, string usuPerfilDescricao)
        {

            UsuarioPerfilBL usuarioPefilBL = new UsuarioPerfilBL();
            IEnumerable<UsuarioPerfil> listaPerfilUsuario = new List<UsuarioPerfil>();

            try
            {
                listaPerfilUsuario = usuarioPefilBL.ConsultarPerfilUsuario(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

            return Ok(listaPerfilUsuario);
        }

    }

}
