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
    public class LogErroDal : BaseRepository
    {
        #region CRUD

        public Int64 Insert(LogErro LogErro)
        {
            Int64 recordId = new DapperSqlHelper().InsertWithReturnId(LogErro);
            return recordId;
        }

        public LogErro Update(LogErro LogErro)
        {
            bool update = new DapperSqlHelper().Update<LogErro>(LogErro);
            return LogErro;
        }

        public bool Delete(long pLogErro)
        {
            return new DapperSqlHelper().Delete<LogErro>(new LogErro() { LogErroId = pLogErro });
        }

        public IList<LogErro> GetAll()
        {
            return new DapperSqlHelper().GetAll<LogErro>();
        }

        public LogErro GetCliente(Int64 pLogErroId)
        {
            var p = new DynamicParameters();
            p.Add("@pLogErroId", pLogErroId, DbType.String, null);

            var Cliente = new DapperSqlHelper().QuerySP<LogErro>("ConsultarLogErro", p, null, null, false, 0);
            return (LogErro)Cliente.FirstOrDefault();
        }

        #endregion
    }
}
