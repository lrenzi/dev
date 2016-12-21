using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("DocumentoClienteDados")]
    public class DocumentoClienteDados
    {
        [DapperAttribute.Key]
        public Int64 DocCliDadosId { get; set; }
        public string DocCliDadosValor { get; set; }
        public Int64 ClienteId { get; set; }
        public Int64 TipoInfoCliId { get; set; }
        //public DocumentoClienteDadosDoc DocumentoClienteDadosDoc { get; set; }

        [DapperAttribute.Write(false)]
        public IList<DocumentoCliente> DocumentosCliente { get; set; }
    }
}
