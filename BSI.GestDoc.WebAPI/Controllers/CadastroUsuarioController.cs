using System;
using System.Web.Http;
using BSI.GestDoc.BusinessLogic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Usuario")]
    public class CadastroUsuarioController : ApiController
    {

        [System.Web.Http.Route("CadastrarUsuario")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult CadastrarUsuario(string userNameUsuario, string nomeUsuario, string emailUsuario,
                                                    string perfilUsuario, string senhaUsuario, string clientId)
        {

            UsuarioBL usuarioBL = new UsuarioBL();
            dynamic retorno = null;

            try
            {
                retorno = usuarioBL.CadastrarUsuario(userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, senhaUsuario, clientId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

            return Ok(retorno);
        }

    }

}
