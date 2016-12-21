using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Dapper.Helper
{
    public static class SqlHelper
    {
        public static bool Insert<T>(T parameter) where T : class
        {
            return ContribSql.Insert<T>(parameter);
        }

        public static Int64 InsertWithReturnId<T>(T parameter) where T : class
        {
            return ContribSql.InsertWithReturnId<T>(parameter);
        }

        public static bool Update<T>(T parameter) where T : class
        {
            return ContribSql.Update<T>(parameter);
        }


        public static bool Delete<T>(DapperExtensions.PredicateGroup predicate) where T : class
        {
            return DapperSql.Delete<T>(predicate);
        }

        public static IList<T> GetAll<T>() where T : class
        {
            return DapperSql.GetAll<T>();
        }

        public static T Find<T>(DapperExtensions.PredicateGroup predicate) where T : class
        {
            return DapperSql.Find<T>(predicate);
        }

        public static IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null,
            dynamic outParam = null, System.Data.SqlClient.SqlTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null) where T : class
        {
            return DapperSql.QuerySP<T>(storedProcedure, param, outParam, transaction, buffered, commandTimeout);
        }


        public static IEnumerable<T> QuerySP<T, Y, Z>(string storedProcedure, dynamic param = null,
           dynamic outParam = null, System.Data.SqlClient.SqlTransaction transaction = null,
           bool buffered = true, int? commandTimeout = null) where T : class
        {
            return DapperSql.QuerySP<T, Y, Z>(storedProcedure, param, outParam, transaction, buffered, commandTimeout);
        }

        public static System.Data.SqlClient.SqlConnection getConnection()
        {
            return DapperSql.getConnection();
        }
    }
}
