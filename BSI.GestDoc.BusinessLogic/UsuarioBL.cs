using BSI.GestDoc.Repository.DAL;

namespace BSI.GestDoc.BusinessLogic
{
    public class UsuarioBL
    {
        public UsuarioBL()
        {
        }

        
        public dynamic CadastrarUsuario(string userNameUsuario, string nomeUsuario, string emailUsuario, 
            string perfilUsuario, string senhaUsuario, string clientId)
        {
            UsuarioDal UsuarioDal = new UsuarioDal();
            
            //usado para cadastro do usuario
            string usuarioAtivo = "1";

           return UsuarioDal.CadastrarUsuario(userNameUsuario, nomeUsuario, emailUsuario, perfilUsuario, Util.UtilCriptografia.GetMd5Hash(senhaUsuario), usuarioAtivo, clientId);
            
        }

        public void ConsultarPerfilUsuario(string usuPerfilId, string clienteId, string usuPerfilNome, string usuPerfilDescricao)
        {
            UsuarioDal UsuarioDal = new UsuarioDal();
            UsuarioPerfilDal UsuPerfilDal = new UsuarioPerfilDal();

            UsuPerfilDal.ConsultarUsuarioPerfil(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao);
            
         
        }
    }
}
