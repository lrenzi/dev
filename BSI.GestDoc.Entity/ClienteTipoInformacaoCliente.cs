using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("ClienteTipoInformacaoCliente")]
    public class ClienteTipoInformacaoCliente
    {
        [DapperAttribute.Key]
        public int CliTipoInfoCliId { get; set; }
        public Int64 ClienteId { get; set; }
        public int TipoInfoCliId { get; set; }
    }
}
