using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("DocumentoClienteDadosDoc")]
    public class DocumentoClienteDadosDoc
    {
        [DapperAttribute.Key]
        public Int64 DocCliDadosDocId { get; set; }
        public Int64 DocCliDadosId { get; set; }
        public Int64 DocClienteId { get; set; }
        
        //public DocumentoClienteDados DocumentoClienteDados { get; set; }

    }
}
