using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class UsuarioDal
    {
        #region CRUD

        public Int64 Insert(Usuario Usuario)
        {
            Int64 recordId = SqlHelper.InsertWithReturnId(Usuario);
            return recordId;
        }

        public Usuario Update(Usuario Usuario)
        {
            bool update = SqlHelper.Update<Usuario>(Usuario);
            return Usuario;
        }

        public bool Delete(long pUsuarioId)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<Usuario>(f => f.UsuarioId, Operator.Eq, pUsuarioId, true));

            return SqlHelper.Delete<Usuario>(pg);
        }

        public IList<Usuario> GetAll()
        {
            return SqlHelper.GetAll<Usuario>();
        }

        public Usuario GetUsuario(long pUsuarioId)
        {
            var p = new DynamicParameters();
            p.Add("@pUsuarioId", pUsuarioId, DbType.Int64, ParameterDirection.Input, null);
            var usuario = SqlHelper.QuerySP<Usuario>("ConsultarUsuario", p, null, null, false, 0);
            return (Usuario)usuario.FirstOrDefault();
        }

        #endregion

        #region Customizados

        /// <summary>
        /// Consulta os usuarios cadastrados na base de dados
        /// </summary>
        /// <param name="usuPerfilId"></param>
        /// <param name="clienteId"></param>
        /// <param name="usuPerfilNome"></param>
        /// <param name="usuPerfilDescricao"></param>
        /// <returns></returns>
        public IEnumerable<Usuario> ConsultarUsuario(string usuarioId, string usuarioLogin, string usuarioNome, string usuarioEmail,
                                                    string usuarioSenha, string usuarioAtivo, string usuPerfilId, string usuClienteId, string allowedOrigin)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@pUsuarioId", usuarioId, DbType.Int16, null);
            parameters.Add("@pUsuarioLogin", usuarioLogin, DbType.String, null);
            parameters.Add("@pUsuarioNome", usuarioNome, DbType.String, null);
            parameters.Add("@pUsuarioEmail", usuarioEmail, DbType.String, null);
            parameters.Add("@pUsuarioSenha", usuarioSenha, DbType.String, null);
            parameters.Add("@pUsuarioAtivo", usuarioAtivo, DbType.Byte, null);
            parameters.Add("@pUsuPerfilId", usuPerfilId, DbType.Int16, null);
            parameters.Add("@pClienteId", usuClienteId, DbType.Int16, null);
            parameters.Add("@pAllowedOrigin", allowedOrigin, DbType.String, null);

            SqlConnection connection = SqlHelper.getConnection();
            Usuario usuarioLogado = new Usuario();
            IEnumerable<Usuario> listaUsuarios = null;

            using (SqlMapper.GridReader reader = connection.QueryMultiple("ConsultarUsuario", parameters, commandType: CommandType.StoredProcedure))
            {
                //recupera dados do cliente e informações referenciadas
                listaUsuarios = reader.Read<Usuario, UsuarioPerfil, Usuario>((usuario, usuarioPerfil) =>
                {
                    usuario.UsuarioPerfil = usuarioPerfil;
                    return usuario;
                }, splitOn: "UsuarioId, UsuPerfilNome");
            }

            return listaUsuarios;

        }

        /*
        /// <summary>
        /// Insere novo usuario na base de dados
        /// </summary>
        /// <param name="userNameUsuario"></param>
        /// <param name="nomeUsuario"></param>
        /// <param name="emailUsuario"></param>
        /// <param name="perfilUsuario"></param>
        /// <param name="senhaUsuario"></param>
        /// <param name="usuarioAtivo"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
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

        

        /// <summary>
        /// Altera os dados do usuario
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public dynamic AlterarUsuario(string usuarioId, string usuarioLogin, string usuarioNome, string usuarioEmail,
                                                    string usuarioSenha, string usuarioAtivo, string usuPerfilId, string usuClienteId)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@pUsuarioId", usuarioId, DbType.Int16, null);
            parameters.Add("@pUsuarioLogin", usuarioLogin, DbType.String, null);
            parameters.Add("@pUsuarioNome", usuarioNome, DbType.String, null);
            parameters.Add("@pUsuarioEmail", usuarioEmail, DbType.String, null);
            parameters.Add("@pUsuarioSenha", usuarioSenha, DbType.String, null);
            parameters.Add("@pUsuarioAtivo", usuarioAtivo, DbType.Byte, null);
            parameters.Add("@pUsuPerfilId", usuPerfilId, DbType.Int16, null);
            parameters.Add("@pClienteId", usuClienteId, DbType.Int16, null);

            var retornoAlteracao = SqlHelper.QuerySP<Usuario>("ManterUsuario", parameters);


            return retornoAlteracao;
        }
        */

        #endregion
    }
}
