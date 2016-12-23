using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("DocumentoClienteSituacao")]
    public class DocumentoClienteSituacao
    {
        [DapperAttribute.Key]
        public int DocCliSituId { get; set; }
        public string DocCliSituDescricao { get; set; }
        public int DocCliTipoId { get; set; }
        public int DocCliSituOrdemApresent { get; set; }

        [DapperAttribute.Write(false)]
        public DocumentoClienteTipo DocumentoClienteTipo { get; set; }

        [DapperAttribute.Write(false)]
        public DocumentoCliente DocumentoCliente { get; set; }

        [DapperAttribute.Computed]
        [DapperAttribute.Write(false)]
        public string NomeArquivoSalvoAux { get; set; }
    }
}
