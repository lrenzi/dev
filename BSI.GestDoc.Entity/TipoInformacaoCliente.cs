using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("TipoInformacaoCliente")]
    public class TipoInformacaoCliente
    {
        [DapperAttribute.Key]
        public int TipoInfoCliId { get; set; }
        public string TipoInfoCliDescricao { get; set; }
    }
}
