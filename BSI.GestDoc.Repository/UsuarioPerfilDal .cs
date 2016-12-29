﻿using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace BSI.GestDoc.Repository.DAL
{
    public class UsuarioPerfilDal
    {
        /// <summary>
        /// Consulta os perfis de usuarios
        /// </summary>
        /// <param name="usuPerfilId"></param>
        /// <param name="clienteId"></param>
        /// <param name="usuPerfilNome"></param>
        /// <param name="usuPerfilDescricao"></param>
        /// <returns></returns>
        public IEnumerable<UsuarioPerfil> ConsultarUsuarioPerfil(string usuPerfilId, string clienteId, string usuPerfilNome, string usuPerfilDescricao )
        {

            var parameters = new DynamicParameters();
            parameters.Add("@pUsuPerfilId", usuPerfilId, DbType.Int16, null);
            parameters.Add("@pClienteId", clienteId, DbType.Int16, null);
            parameters.Add("@pUsuPerfilNome", usuPerfilNome, DbType.String, null);
            parameters.Add("@pUsuPerfilDescricao", usuPerfilDescricao, DbType.String, null);            
            
            var listaUsuarioPerfil = SqlHelper.QuerySP<UsuarioPerfil>("ConsultarUsuarioPerfil", parameters);


            return listaUsuarioPerfil;
        }        
    }
}
