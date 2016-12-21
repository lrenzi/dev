using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    public class DocumentoClienteTipo
    {
        [DapperAttribute.Key]
        public int DocCliTipoId { get; set; }
        public int ClienteId { get; set; }
        public string DocCliTipoNome { get; set; }
        public string DocCliTipoDescricao { get; set; }
        public int DocCliTipoOrdemApresent { get; set; }
        public Cliente Cliente { get; set; }
        public IEnumerable<DocumentoClienteSituacao> ListaSituacaoDocumentoCliente { get; set; }
        //Auxiliar
        public string Reenvio { get; set; }
    }
}
