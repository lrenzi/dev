using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.Base;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class UsuarioPerfilDal : BaseRepository
    {
        #region CRUD

        public Int64 Insert(UsuarioPerfil UsuarioPerfil)
        {
            Int64 recordId = new DapperSqlHelper().InsertWithReturnId(UsuarioPerfil);
            return recordId;
        }

        public UsuarioPerfil Update(UsuarioPerfil UsuarioPerfil)
        {
            bool update = new DapperSqlHelper().Update<UsuarioPerfil>(UsuarioPerfil);
            return UsuarioPerfil;
        }

        public bool Delete(int pUsuPerfilId)
        {
            return new DapperSqlHelper().Delete<UsuarioPerfil>(new UsuarioPerfil() { UsuPerfilId = pUsuPerfilId });
        }

        public IList<UsuarioPerfil> GetAll()
        {
            return new DapperSqlHelper().GetAll<UsuarioPerfil>();
        }

        public UsuarioPerfil GetUsuarioPerfil(Int64 pUsuPerfilId)
        {
            var p = new DynamicParameters();
            p.Add("@pUsuPerfilId", pUsuPerfilId, DbType.Int64, null);

            var Cliente = new DapperSqlHelper().QuerySP<UsuarioPerfil>("ConsultarUsuarioPerfil", p, null, null, false, 0);
            return (UsuarioPerfil)Cliente.FirstOrDefault();
        }

        #endregion

        #region Customizados

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
            
            var listaUsuarioPerfil = new DapperSqlHelper().QuerySP<UsuarioPerfil>("ConsultarUsuarioPerfil", parameters);


            return listaUsuarioPerfil;
        }

        #endregion
    }
}
