using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using DapperExtensions;
using Dapper;
using System.Data;
using Dapper.Mapper;
using System;

namespace BSI.Dapper.Helper
{
    public abstract class DapperExtensionsSql : DapperBaseSql, IDisposable
    {
        public DapperExtensionsSql() : base() { }

        public IList<T> GetAll<T>() where T : class
        {
            using (var sqlConnection = NewSqlConnection)
            {
                var result = sqlConnection.GetList<T>();
                sqlConnection.Close();
                return result.ToList();
            }
        }

        public T Find<T>(PredicateGroup predicate) where T : class
        {
            using (var sqlConnection = NewSqlConnection)
            {
                var result = sqlConnection.GetList<T>(predicate).FirstOrDefault();
                sqlConnection.Close();
                return result;
            }
        }

        public IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null,
            dynamic outParam = null, SqlTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null) where T : class
        {
            IEnumerable<T> output = null;
            using (var connection = NewSqlConnection)
            {
                output = connection.Query<T>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);
                return output.ToList();
            }            
        }


        public IEnumerable<T> QuerySP<T, Y, Z>(string storedProcedure, dynamic param = null,
           dynamic outParam = null, SqlTransaction transaction = null,
           bool buffered = true, int? commandTimeout = null) where T : class
        {
            using (var connection = NewSqlConnection)
            {
                var output = connection.Query<T, Y, Z>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure, splitOn: "UsuarioId,UsuarioLogin,UsuarioNome,UsuarioEmail,UsuPerfilId,UsuPerfilNome,UsuPerfilDescricao,ClienteId,ClienteNome,ClientePastaDocumentos,ClienteImagemLogoDesktop,ClienteImagemLogoMobile,ClienteCorPadrao");
                connection.Close();
                return output.ToList();
            }
        }

        private void CombineParameters(ref dynamic param, dynamic outParam = null)
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

        void IDisposable.Dispose()
        {

        }
    }
}
