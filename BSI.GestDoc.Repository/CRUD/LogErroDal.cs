using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class LogErroDal
    {
        #region CRUD

        public Int64 Insert(LogErro LogErro)
        {
            Int64 recordId = SqlHelper.InsertWithReturnId(LogErro);
            return recordId;
        }

        public LogErro Update(LogErro LogErro)
        {
            bool update = SqlHelper.Update<LogErro>(LogErro);
            return LogErro;
        }

        public bool Delete(long pLogErro)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<LogErro>(f => f.LogErroId, Operator.Eq, pLogErro, true));

            return SqlHelper.Delete<LogErro>(pg);
        }

        public IList<LogErro> GetAll()
        {
            return SqlHelper.GetAll<LogErro>();
        }

        public LogErro GetCliente(Int64 pLogErroId)
        {
            var p = new DynamicParameters();
            p.Add("@pLogErroId", pLogErroId, DbType.String, null);

            var Cliente = SqlHelper.QuerySP<LogErro>("ConsultarLogErro", p, null, null, false, 0);
            return (LogErro)Cliente.FirstOrDefault();
        }

        #endregion
    }
}
