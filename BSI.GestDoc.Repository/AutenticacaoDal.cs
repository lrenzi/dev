using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.GestDoc.Repository
{
    public class AutenticacaoDal
    {
        public async Task<Usuario> Efetuarlogin(String usuarioLogin, String usuarioSenha)
        {
            Usuario usuarioRetorno = null;

            var pIn = new DynamicParameters();
            pIn.Add("@UsuarioLogin", usuarioLogin, DbType.String, null);
            pIn.Add("@UsuarioSenha", usuarioSenha, DbType.String, null);
            pIn.Add("@StatusProcessamento", null, DbType.Int16, ParameterDirection.Output);
            pIn.Add("@MensagemProcessamento", null, DbType.String, ParameterDirection.Output, 200);

            //var retornoAutenticacao = SqlHelper.QuerySP<Cliente>("AutenticarUsuario", pIn, null, null, false, 0).FirstOrDefault();

            var retornoAutenticacao = SqlHelper.QuerySP<Usuario, UsuarioPerfil,Cliente>("AutenticarUsuario", pIn, null, null, false, 0).FirstOrDefault();


            if (retornoAutenticacao == null)
            {
                usuarioRetorno = new Usuario();
            }
            else
            {
                usuarioRetorno = (Usuario)retornoAutenticacao;
            }
            usuarioRetorno.StatusProcessamento = pIn.Get<Int16>("@StatusProcessamento");
            usuarioRetorno.MensagemProcessamento = pIn.Get<string>("@MensagemProcessamento");

            return usuarioRetorno;
        }
    }
}
