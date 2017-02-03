using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.CRUD;
using System.Collections.Generic;
using System.Linq;
using System;
using BSI.GestDoc.BusinessLogic.Base;

namespace BSI.GestDoc.BusinessLogic
{
    public class UsuarioBL : BaseBL
    {
        public UsuarioBL()
        {
        }

        /// <summary>
        /// Insere novo usuario na base de dados
        /// </summary>
        /// <param name="userNameUsuario"></param>
        /// <param name="nomeUsuario"></param>
        /// <param name="emailUsuario"></param>
        /// <param name="perfilUsuario"></param>
        /// <param name="senhaUsuario"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public dynamic CadastrarUsuario(string userNameUsuario, string nomeUsuario, string emailUsuario,
            string perfilUsuario, string senhaUsuario, string clientId)
        {
            UsuarioDal UsuarioDal = new UsuarioDal();
            IEnumerable<Usuario> userNameSingle = null;
            dynamic retorno = null;
            Usuario usuario = new Usuario();
            //Consulta o usuário pelo "userName", não é permitido o cadastro de dois usuários com mesmo "userName"
            userNameSingle = this.ConsultarUsuario(null, userNameUsuario, null, null, null, null, null, null);

            //valida se o username ja existe
            if (userNameSingle == null || userNameSingle.Count() == 0)
            {
                //usado para cadastro do usuario

                usuario.UsuarioLogin = userNameUsuario;
                usuario.UsuarioNome = nomeUsuario;
                usuario.UsuarioEmail = emailUsuario;
                usuario.UsuarioSenha = Util.UtilCriptografia.GetMd5Hash(senhaUsuario);
                usuario.UsuarioAtivo = true;
                usuario.UsuPerfilId = int.Parse(perfilUsuario);
                usuario.ClienteId = int.Parse(clientId);
                usuario.AllowedOrigin = "*";
                retorno = UsuarioDal.Insert(usuario);
                retorno = UsuarioDal.GetUsuario(retorno);
            }
            else
            {
                retorno = "Nome de usuário já existente!";
            }



            return retorno;
        }

        /// <summary>
        /// Consulta os perfis de usuarios 
        /// </summary>
        /// <param name="usuPerfilId"></param>
        /// <param name="clienteId"></param>
        /// <param name="usuPerfilNome"></param>
        /// <param name="usuPerfilDescricao"></param>
        public void ConsultarPerfilUsuario(string usuPerfilId, string clienteId, string usuPerfilNome, string usuPerfilDescricao)
        {
            UsuarioDal UsuarioDal = new UsuarioDal();
            UsuarioPerfilDal UsuPerfilDal = new UsuarioPerfilDal();

            UsuPerfilDal.ConsultarUsuarioPerfil(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao);
        }

        /// <summary>
        /// Consulta usuarios cadastrados no sistema
        /// </summary>
        /// <param name="usuarioLogin"></param>
        /// <param name="usuarioNome"></param>
        /// <param name="usuarioEmail"></param>
        /// <param name="usuarioSenha"></param>
        /// <param name="usuarioAtivo"></param>
        /// <param name="usuPerfilId"></param>
        /// <param name="usuClienteId"></param>
        /// <returns></returns>
        public IEnumerable<Usuario> ConsultarUsuario(string usuarioId, string usuarioLogin, string usuarioNome, string usuarioEmail,
                                                    string usuarioSenha, string usuarioAtivo, string usuPerfilId, string usuClienteId)
        {
            return new UsuarioDal().ConsultarUsuario(usuarioId, usuarioLogin, usuarioNome, usuarioEmail,
                                                    usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId, null);
        }

        public Usuario ConsultarUsuarioById(string usuarioId)
        {
            UsuarioDal UsuarioDal = new UsuarioDal();
            return UsuarioDal.GetUsuario(long.Parse(usuarioId));
        }

        /// <summary>
        /// Altera dados do Usuário
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public dynamic AlterarUsuario(string usuarioId, string usuarioLogin, string usuarioNome, string usuarioEmail,
                                                    string usuarioSenha, string usuarioAtivo, string usuPerfilId, string usuClienteId)
        {
            UsuarioDal UsuarioDal = new UsuarioDal();
            Usuario usuario = new Usuario();
            //usuarioAtivo = usuarioAtivo != null && usuarioAtivo.Trim() != "" && usuarioAtivo == "true" ? "1" : "0";
            usuario = UsuarioDal.GetUsuario(long.Parse(usuarioId));

            //usado para cadastro do usuario
            usuario.UsuarioId = long.Parse(usuarioId);
            usuario.UsuarioLogin = usuarioLogin;
            usuario.UsuarioNome = usuarioNome;
            usuario.UsuarioEmail = usuarioEmail;
            if (!string.IsNullOrEmpty(usuarioSenha))
                usuario.UsuarioSenha = Util.UtilCriptografia.GetMd5Hash(usuarioSenha);
            usuario.UsuarioAtivo = bool.Parse(usuarioAtivo != null && usuarioAtivo.Trim() != "" && usuarioAtivo == "true" ? "true" : "false");
            usuario.UsuPerfilId = int.Parse(usuPerfilId);
            usuario.ClienteId = int.Parse(usuClienteId);
            return UsuarioDal.Update(usuario);

            /*return UsuarioDal.AlterarUsuario(usuarioId, usuarioLogin, usuarioNome, usuarioEmail,
                                                    Util.UtilCriptografia.GetMd5Hash(usuarioSenha), usuarioAtivo, usuPerfilId, usuClienteId);*/
        }
    }
}
