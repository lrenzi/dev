using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using DapperExtensions;
using Dapper;
using System.Data;
using Dapper.Mapper;

namespace BSI.Dapper.Helper
{
    public class DapperSql : baseSql
    {

        public static bool Delete<T>(PredicateGroup predicate) where T : class
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
              //  sqlConnection.Open();
                sqlConnection.Delete<T>(predicate);
                sqlConnection.Close();
                return true;
            }
        }

        public static IList<T> GetAll<T>() where T : class
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
               // sqlConnection.Open();
                var result = sqlConnection.GetList<T>();
                sqlConnection.Close();
                return result.ToList();
            }
        }

        public static T Find<T>(PredicateGroup predicate) where T : class
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
               // sqlConnection.Open();
                var result = sqlConnection.GetList<T>(predicate).FirstOrDefault();
                sqlConnection.Close();
                return result;
            }
        }

        public static IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null,
            dynamic outParam = null, SqlTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null) where T : class
        {

            IEnumerable<T> output = null;

            using (var connection = new SqlConnection(ConnectionString))
            {
                output = connection.Query<T>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);

                return output.ToList();
            }            
        }


        public static IEnumerable<T> QuerySP<T, Y, Z>(string storedProcedure, dynamic param = null,
           dynamic outParam = null, SqlTransaction transaction = null,
           bool buffered = true, int? commandTimeout = null) where T : class
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var output = connection.Query<T, Y, Z>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure, splitOn: "UsuarioId,UsuarioLogin,UsuarioNome,UsuarioEmail,UsuPerfilId,UsuPerfilNome,UsuPerfilDescricao,ClienteId,ClienteNome,ClientePastaDocumentos,ClienteImagemLogoDesktop,ClienteImagemLogoMobile,ClienteCorPadrao");

                connection.Close();

                return output.ToList();
            }
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
        }

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
