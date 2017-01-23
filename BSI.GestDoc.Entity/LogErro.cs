using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("LogErro")]
    public class LogErro
    {
        [DapperAttribute.Key]
        public Int64 LogErroId { get; set; }
        public string Descricao { get; set; }
        public string Trace { get; set; }
        public DateTime Data { get; set; }
        public int Ambiente { get; set; }
        public string HostName { get; set; }
        public int TipoMensagem { get; set; }
    }
}
