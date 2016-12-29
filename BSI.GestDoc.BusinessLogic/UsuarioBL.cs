using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.DAL;
using System.Collections.Generic;

namespace BSI.GestDoc.BusinessLogic
{
    public class UsuarioBL
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
            
            //usado para cadastro do usuario
            string usuarioAtivo = "1";

           return UsuarioDal.CadastrarUsuario(userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, Util.UtilCriptografia.GetMd5Hash(senhaUsuario), usuarioAtivo, clientId);
            
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
            UsuarioDal UsuarioDal = new UsuarioDal();
            
            return UsuarioDal.ConsultarUsuario(usuarioId, usuarioLogin, usuarioNome, usuarioEmail,
                                                    usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId);
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

            return UsuarioDal.AlterarUsuario(usuarioId, usuarioLogin, usuarioNome, usuarioEmail,
                                                    usuarioSenha, usuarioAtivo, usuPerfilId, usuClienteId);
        }
    }
}
