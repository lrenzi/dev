using BSI.GestDoc.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Dapper.Helper
{
    public abstract class baseSql
    {
        public static string ConnectionString
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
    }
}
