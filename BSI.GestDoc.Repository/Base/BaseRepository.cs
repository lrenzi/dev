using BSI.Dapper.Helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.Repository.Base
{
    public class BaseRepository
    {
        public BaseRepository() {

        }

        public BaseRepository(DbTransaction scope_)
        {
            DapperSqlHelper.Transaction = scope_;
        }

        public DbTransaction Scope {
            get {
                return DapperSqlHelper.Transaction;
            }
        }
        private DapperSqlHelper Helper { get; set; }

        public void BeginTransaction() {
            DapperSqlHelper.BeginTransaction();
        }

        public void CommitTransaction()
        {
            DapperSqlHelper.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            DapperSqlHelper.RollbackTransaction();
        }
    }
}
