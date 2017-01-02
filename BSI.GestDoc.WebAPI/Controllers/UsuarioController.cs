using System;
using System.Web.Http;
using BSI.GestDoc.BusinessLogic;
using System.Collections.Generic;
using BSI.GestDoc.Entity;
using System.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.GestDoc.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        /// <summary>
        /// Insere novo usuario na base de  dados
        /// </summary>
        /// <param name="userNameUsuario"></param>
        /// <param name="nomeUsuario"></param>
        /// <param name="emailUsuario"></param>
        /// <param name="perfilUsuario"></param>
        /// <param name="senhaUsuario"></param>
        /// <param name="clientId"></param>
        /// <returns>IEnumerable<Usuario> - sucesso no cadasto</returns>
        /// <returns>String(message) - Nome do usuário já existe</returns>
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


        /// <summary>
        /// Consulta usuarios da base de dados
        /// </summary>
        /// <param name="userNameUsuario"></param>
        /// <param name="nomeUsuario"></param>
        /// <param name="emailUsuario"></param>
        /// <param name="perfilUsuario"></param>
        /// <param name="senhaUsuario"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [System.Web.Http.Route("Consultar")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Consultar(string usuarioId, string usuarioLogin, string usuarioNome, string usuarioEmail,
                                                    string usuarioSenha, string usuarioAtivo, string usuPerfilId, string usuClienteId)
        {

            UsuarioBL usuarioBL = new UsuarioBL();

            IEnumerable<Usuario> retorno = null;

            try
            {

                retorno = usuarioBL.ConsultarUsuario(usuarioId, usuarioLogin, usuarioNome, usuarioEmail,
                                                    usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId);

                return Ok(
                            retorno.Select(e => new
                            {
                                e.UsuarioLogin,
                                e.UsuarioNome,
                                e.UsuarioAtivoDescricao,
                                e.UsuarioEmail,
                                e.UsuarioPerfil
                            })
                        );


            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }

        /// <summary>
        /// Altera dados do usuario
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="usuarioLogin"></param>
        /// <param name="usuarioNome"></param>
        /// <param name="usuarioEmail"></param>
        /// <param name="usuarioSenha"></param>
        /// <param name="usuarioAtivo"></param>
        /// <param name="usuPerfilId"></param>
        /// <param name="usuClienteId"></param>
        /// <returns></returns>
        [System.Web.Http.Route("Alterar")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult AlterarUsuario(string usuarioId, string usuarioLogin, string usuarioNome, string usuarioEmail,
                                                    string usuarioSenha, string usuarioAtivo, string usuPerfilId, string usuClienteId)
        {

            UsuarioBL usuarioBL = new UsuarioBL();
            dynamic retorno = null;

            try
            {

                retorno = usuarioBL.AlterarUsuario(usuarioId, usuarioLogin, usuarioNome, usuarioEmail,
                                                    usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

            return Ok(retorno);
        }

    }

}
