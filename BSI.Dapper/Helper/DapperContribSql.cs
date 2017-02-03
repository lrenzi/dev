using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Data;
using Dapper.Mapper;

namespace BSI.Dapper.Helper
{
    public abstract class DapperContribSql : DapperExtensionsSql, IDisposable
    {
        public DapperContribSql() : base() { }

        public bool Insert<T>(T parameter) where T : class
        {
            if (IsTransaction)
                SqlConnection.Insert(parameter, Transaction, ConnectionTimeout);
            else
                SqlConnection.Insert(parameter);
            return true;
        }

        public Int64 InsertWithReturnId<T>(T parameter) where T : class
        {
            if (IsTransaction)
                return SqlConnection.Insert(parameter, Transaction, ConnectionTimeout);
            else
                return SqlConnection.Insert(parameter);
        }

        public bool Update<T>(T parameter) where T : class
        {
            if (IsTransaction)
                SqlConnection.Update(parameter, Transaction, ConnectionTimeout);
            else
                SqlConnection.Update(parameter);
            return true;
        }

        public bool Delete<T>(T parameter) where T : class
        {
            if (IsTransaction)
                SqlConnection.Delete(parameter, Transaction, ConnectionTimeout);
            else
                SqlConnection.Delete(parameter);
            return true;
        }

       
        void IDisposable.Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
