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
    public static class ContribSql
    {
        public static string ConnectionString { get { return "Data Source=bsidatabase.database.windows.net;Initial Catalog=bsidbdesenv;Persist Security Info=True;User ID=sqladmin;pwd=Bsi@admin"; } }

        public static bool Insert<T>(T parameter) where T : class
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                sqlConnection.Insert(parameter);
                sqlConnection.Close();
                return true;
            }
        }

        public static Int64 InsertWithReturnId<T>(T parameter) where T : class
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                var recordId = sqlConnection.Insert(parameter);
                sqlConnection.Close();
                return recordId;
            }
        }

        public static bool Update<T>(T parameter) where T : class
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                sqlConnection.Update(parameter);
                sqlConnection.Close();
                return true;
            }
        }

        /*public static IList<T> GetAll<T>() where T : class
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                //var result = sqlConnection.GetList<T>();
                sqlConnection.Close();
                //return result.ToList();
                return null;
            }
        }*/

        /*public static T Find<T>(PredicateGroup predicate) where T : class
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.GetList<T>(predicate).FirstOrDefault();
                sqlConnection.Close();
                return result;
            }
        }*/

        /*public static bool Delete<T>(PredicateGroup predicate) where T : class
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                sqlConnection.Delete<T>(predicate);
                sqlConnection.Close();
                return true;
            }
        }*/

        /*public static IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null,
            dynamic outParam = null, SqlTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null) where T : class
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            //var output = connection.Query<T>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);

            return null;
        }


        public static IEnumerable<T> QuerySP<T, Y, Z>(string storedProcedure, dynamic param = null,
           dynamic outParam = null, SqlTransaction transaction = null,
           bool buffered = true, int? commandTimeout = null) where T : class
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            var output = connection.Query<T, Y, Z>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure, splitOn: "UsuarioId,UsuarioLogin,UsuarioNome,UsuarioEmail,UsuPerfilId,UsuPerfilNome,UsuPerfilDescricao,ClienteId,ClienteNome,ClientePastaDocumentos,ClienteImagemLogoDesktop,ClienteImagemLogoMobile,ClienteCorPadrao");

            return output;
        }

        private static void CombineParameters(ref dynamic param, dynamic outParam = null)
        {
            if (outParam != null)
            {
                if (param != null)
                {
                    param = new DynamicParameters(param);
                    ((DynamicParameters)param).AddDynamicParams(outParam);
                }
                else
                {
                    param = outParam;
                }
            }
        }*/

        private static int ConnectionTimeout { get; set; }

        private static int GetTimeout(int? commandTimeout = null)
        {
            if (commandTimeout.HasValue)
                return commandTimeout.Value;

            return ConnectionTimeout;
        }
        public static SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }
    }
}
