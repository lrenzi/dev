using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.CRUD;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.GestDoc.BusinessLogic
{
    public class UsuarioPerfilBL
    {
        public UsuarioPerfilBL()
        {
        }

        /// <summary>
        /// Consulta os perfis dos usuarios cadastrados na base de dados
        /// </summary>
        /// <param name="usuPerfilId"></param>
        /// <param name="clienteId"></param>
        /// <param name="usuPerfilNome"></param>
        /// <param name="usuPerfilDescricao"></param>
        /// <returns></returns>
        public IEnumerable<UsuarioPerfil> ConsultarPerfilUsuario(string usuPerfilId, string clienteId, string usuPerfilNome, string usuPerfilDescricao)
        {
            UsuarioDal UsuarioDal = new UsuarioDal();
            UsuarioPerfilDal UsuPerfilDal = new UsuarioPerfilDal();

            //efetua a consulta
            return UsuPerfilDal.ConsultarUsuarioPerfil(usuPerfilId, clienteId, usuPerfilNome, usuPerfilDescricao);
        }
    }
}
