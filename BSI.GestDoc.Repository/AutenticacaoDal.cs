using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.GestDoc.Repository.DAL
{
    public class AutenticacaoDal
    {
        public IList<Token> GetAtllToken()
        {
            return SqlHelper.GetAll<Token>();
        }

        public bool RemoveToken(long TokenId)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<Token>(f => f.TokenId, Operator.Eq, TokenId, true));

            return SqlHelper.Delete<Token>(pg);
        }

        public bool InsertToken(Token Token)
        {
            return SqlHelper.Insert<Token>(Token);
        }

        public async Task<Usuario> Efetuarlogin(String usuarioLogin, String usuarioSenha)
        {
            Usuario usuarioRetorno = null;

            var pIn = new DynamicParameters();
            pIn.Add("@UsuarioLogin", usuarioLogin, DbType.String, null);
            pIn.Add("@UsuarioSenha", usuarioSenha, DbType.String, null);
           
            var retornoAutenticacao = this.QuerySPCustom("AutenticarUsuario", pIn);


            if (retornoAutenticacao == null)
            {
                usuarioRetorno = new Usuario();
            }
            else
            {
                usuarioRetorno = (Usuario)retornoAutenticacao;
            }

            return usuarioRetorno;
        }

        public Usuario QuerySPCustom(String storedProcedure, DynamicParameters pIn)
        {
            SqlConnection connection = SqlHelper.getConnection();
            Usuario usuarioLogado = new Usuario();
            IEnumerable<Usuario> usuarioRetorno = null;

            using (SqlMapper.GridReader reader = connection.QueryMultiple(storedProcedure, pIn, commandType: CommandType.StoredProcedure))
            {
                //recupera StatusProcessamento (1 = usuario com acesso, 1 = usuario inativo e 2 = usuario ou senha invalido / e MensagemProcessamento
                var infosLoginExecucao = reader.Read().ToList()[0];

                var codigoExecucao = ((object[])((System.Collections.Generic.IDictionary<string, object>)infosLoginExecucao).Values)[0]; //codigo de execucao
                var mensagemExecucao = ((object[])((System.Collections.Generic.IDictionary<string, object>)infosLoginExecucao).Values)[1]; //mensagem apos execucao

                //caso usuario ter acesso ao sistema, recupera-se as demais informações do usuario logado 
                if ((int)codigoExecucao == 0)
                {
                    //recupera dados do cliente e informações referenciadas
                    usuarioRetorno = reader.Read<Usuario, UsuarioPerfil, Cliente, Usuario>((usuario, usuarioPerfil, cliente) =>
                    {
                        usuario.UsuarioPerfil = usuarioPerfil;
                        usuario.Cliente = cliente;
                        return usuario;
                    }, splitOn: "UsuarioId, UsuPerfilId, ClienteId");

                    usuarioLogado = (Usuario)usuarioRetorno.ToList()[0];
                }

                usuarioLogado.StatusProcessamento = (int)codigoExecucao;
                usuarioLogado.MensagemProcessamento = (string)mensagemExecucao;
            }

            return usuarioLogado;
        }
    }
}
