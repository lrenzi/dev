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
    public class ClienteDal : BaseRepository
    {
        #region CRUD

        public Int64 Insert(Cliente Cliente)
        {
            Int64 recordId = new DapperSqlHelper().InsertWithReturnId(Cliente);
            return recordId;
        }

        public Cliente Update(Cliente Cliente)
        {
            bool update = new DapperSqlHelper().Update<Cliente>(Cliente);
            return Cliente;
        }

        public bool Delete(long pClienteId)
        {
            return new DapperSqlHelper().Delete<Cliente>(new Cliente() { ClienteId = pClienteId });
        }

        public IList<Cliente> GetAll()
        {
            return new DapperSqlHelper().GetAll<Cliente>();
        }

        public Cliente GetCliente(Int64 pClienteId)
        {
            var p = new DynamicParameters();
            p.Add("@pClienteId", pClienteId, DbType.String, null);

            var Cliente = new DapperSqlHelper().QuerySP<Cliente>("ConsultarCliente", p, null, null, false, 0);
            return (Cliente)Cliente.FirstOrDefault();
        }

        #endregion
    }
}
