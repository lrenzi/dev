using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.Entity
{
    public class DocumentoCliente
    {
        public Int64 DocClienteId { get; set; }
        public Int64 UsuarioId { get; set; }
        public string DocClienteNomeArquivoSalvo { get; set; }
        public DateTime DocClienteDataUpload { get; set; }
        public int DocCliSituId { get; set; }
        public int ClienteId { get; set; }
        public int DocCliTipoId { get; set; }
        public string DocClienteNomeArquivoOriginal { get; set; }
        public string DocClienteTipoArquivo { get; set; }

        public DocumentoClienteSituacao DocumentoClienteSituacao { get; set; }

        public DocumentoClienteTipo DocumentoClienteTipo { get; set; }       
    }
}
