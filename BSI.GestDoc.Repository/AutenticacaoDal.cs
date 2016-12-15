using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.DAL
{
    public class AutenticacaoDal
    {
        public Usuario Efetuarlogin(String usuarioLogin, String usuarioSenha)
        {
            var pIn = new DynamicParameters();
            pIn.Add("@UsuarioLogin", usuarioLogin, DbType.String, null);
            pIn.Add("@UsuarioSenha", usuarioSenha, DbType.String, null);

            var pOut = new DynamicParameters();
            pOut.Add("@StatusProcessamento", DbType.String, null);
            pOut.Add("@MensagemProcessamento", DbType.String, null);

            var retornoAutenticacao = SqlHelper.QuerySP<Usuario>("AutenticarUsuario", pIn, pOut, null, false, 0);
            return (Usuario)retornoAutenticacao;
        }
    }
}
