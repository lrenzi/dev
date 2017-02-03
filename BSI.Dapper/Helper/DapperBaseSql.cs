using BSI.GestDoc.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Dapper.Helper
{
    public abstract class DapperBaseSql
    {
        public DapperBaseSql() {
            ConnectionTimeout = int.Parse(ConfigurationManager.AppSettings["ConnectionTimeout"].ToString());
            
        }


        private static string ConnectionString
        {
            get
            {
                switch (Ambiente.AmbienteExecucao)
                {
                    case GestDoc.Util.Enumeradores.enumAmbiente.Desenvolvimento:
                        return ConfigurationManager.ConnectionStrings["DBGRAFOMETRIA_DES"].ConnectionString;
                    case GestDoc.Util.Enumeradores.enumAmbiente.Homologação:
                        return ConfigurationManager.ConnectionStrings["DBGRAFOMETRIA_HOM"].ConnectionString;
                    case GestDoc.Util.Enumeradores.enumAmbiente.Produção:
                        return ConfigurationManager.ConnectionStrings["DBGRAFOMETRIA_PRO"].ConnectionString;
                    default:
                        throw new Exception("String de conexão não definido.");
                }

            }
        }

        private static DbTransaction transaction { get; set; }

        private static SqlConnection sqlConnection { get; set; }

        public static DbTransaction Transaction { get { return transaction; } set { transaction = value; } }

        private static bool IsBegin { get; set; }

        public static DbTransaction BeginTransaction()
        {
            if (sqlConnection == null)
                sqlConnection = new SqlConnection(ConnectionString);
            if (sqlConnection.State != System.Data.ConnectionState.Open)
                sqlConnection.Open();

            if (transaction == null || (transaction != null && !IsBegin))
            {
                IsBegin = true;
                FuiChamado = false;
                transaction = sqlConnection.BeginTransaction();
            }else { FuiChamado = true; }

            return transaction;
        }

        private static bool FuiChamado { get; set; }

        protected static bool IsTransaction {
            get{
                if (transaction != null && IsBegin)
                    return true;
                return false;
            }
        }

        public static void CommitTransaction()
        {
            if (transaction != null && IsBegin && !FuiChamado)
            {
                transaction.Commit();
                IsBegin = false;
                FuiChamado = false;
                Transaction = null;
                sqlConnection = null;
            }
        }

        public static void RollbackTransaction()
        {
            if (transaction != null && IsBegin)
            {
                transaction.Rollback();
                IsBegin = false;
            }
        }

        public SqlConnection SqlConnection
        {
            get
            {
                {
                    if (sqlConnection == null || (sqlConnection != null && sqlConnection.State != System.Data.ConnectionState.Open))
                        sqlConnection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                    if (sqlConnection.State != System.Data.ConnectionState.Open)
                        sqlConnection.Open();
                    return sqlConnection;
                }
            }
        }

        public SqlConnection NewSqlConnection
        {
            get
            {
                {
                    return new SqlConnection(ConnectionString);
                }
            }
        }

        protected static int ConnectionTimeout { get; set; }

        private static int GetTimeout(int? commandTimeout = null)
        {
            if (commandTimeout.HasValue)
                return commandTimeout.Value;

            return ConnectionTimeout;
        }
    }
}
