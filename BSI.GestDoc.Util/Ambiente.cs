using BSI.GestDoc.Util.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.Util
{
    public static class Ambiente
    {
        public static enumAmbiente AmbienteExecucao
        {
            get
            {
                string ambiente = System.Configuration.ConfigurationManager.AppSettings["Ambiente"];
                switch(ambiente)
                {
                    case "1":
                        return enumAmbiente.Desenvolvimento;
                    case "2":
                        return enumAmbiente.Homologação;
                    case "3":
                        return enumAmbiente.Produção;
                    default:
                        throw new Exception("Ambiente de execução não definido.");
                }
            }
        }
    }
}
