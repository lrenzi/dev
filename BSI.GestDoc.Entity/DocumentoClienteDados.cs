using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.Entity
{
    public class DocumentoClienteDados
    {
        public Int64 DocCliDadosId { get; set; }
        public string DocCliDadosValor { get; set; }
        public Int64 ClienteId { get; set; }
        public Int64 TipoInfoCliId { get; set; }
        //public DocumentoClienteDadosDoc DocumentoClienteDadosDoc { get; set; }
        public IList<DocumentoCliente> DocumentosCliente { get; set; }
    }
}
