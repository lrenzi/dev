using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BSI.GestDoc.Repository.DAL
{
    public class UsuarioDal
    {
        
        public dynamic CadastrarUsuario(string userNameUsuario, string nomeUsuario, string emailUsuario, 
            string perfilUsuario, string senhaUsuario, string usuarioAtivo, string clientId)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@pUsuarioLogin", userNameUsuario, DbType.String, null);
            parameters.Add("@pUsuarioNome", nomeUsuario, DbType.String, null);
            parameters.Add("@pUsuarioEmail", emailUsuario, DbType.String, null);
            parameters.Add("@pUsuarioSenha", senhaUsuario, DbType.String, null);
            parameters.Add("@pUsuarioAtivo", usuarioAtivo, DbType.String, null);
            parameters.Add("@pUsuPerfilId", perfilUsuario, DbType.String, null);
            parameters.Add("@pClienteId", clientId, DbType.String, null);
            
            var retornoCadastro = SqlHelper.QuerySP<Usuario>("ManterUsuario", parameters);


            return retornoCadastro;
        }
    }
}
