using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.Base;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class UsuarioDal : BaseRepository
    {
        #region CRUD

        public Int64 Insert(Usuario Usuario)
        {
            Int64 recordId = new DapperSqlHelper().InsertWithReturnId(Usuario);
            return recordId;
        }

        public Usuario Update(Usuario Usuario)
        {
            bool update = new DapperSqlHelper().Update<Usuario>(Usuario);
            return Usuario;
        }

        public bool Delete(long pUsuarioId)
        {
            return new DapperSqlHelper().Delete<Usuario>(new Usuario() { UsuarioId = pUsuarioId });
        }

        public IList<Usuario> GetAll()
        {
            return new DapperSqlHelper().GetAll<Usuario>();
        }

        public Usuario GetUsuario(long pUsuarioId)
        {
            var p = new DynamicParameters();
            p.Add("@pUsuarioId", pUsuarioId, DbType.Int64, ParameterDirection.Input, null);
            var usuario = new DapperSqlHelper().QuerySP<Usuario>("ConsultarUsuario", p, null, null, false, 0);
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

            
            Usuario usuarioLogado = new Usuario();
            IEnumerable<Usuario> listaUsuarios = null;

            using (var connection = new DapperSqlHelper().NewSqlConnection)
            {
                using (SqlMapper.GridReader reader = connection.QueryMultiple("ConsultarUsuario", parameters, commandType: CommandType.StoredProcedure))
                {
                    //recupera dados do cliente e informações referenciadas
                    listaUsuarios = reader.Read<Usuario, UsuarioPerfil, Usuario>((usuario, usuarioPerfil) =>
                    {
                        usuario.UsuarioPerfil = usuarioPerfil;
                        return usuario;
                    }, splitOn: "UsuarioId, UsuPerfilNome");
                }
            }

            return listaUsuarios;

        }



        #endregion
    }
}
