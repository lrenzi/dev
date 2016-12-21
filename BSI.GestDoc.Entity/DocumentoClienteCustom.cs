using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    public class DocumentoClienteCustom: DocumentoCliente
    {
        public DocumentoClienteSituacao DocumentoClienteSituacao { get; set; }

        public DocumentoClienteTipo DocumentoClienteTipo { get; set; }

        public string NomeArquivoSalvoAux { get; set; }


    }
}
