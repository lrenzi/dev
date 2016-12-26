using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("DocumentoCliente")]
    public class DocumentoCliente 
    {
        [DapperAttribute.Key]
        public Int64 DocClienteId { get; set; }
        [DapperAttribute.Computed]
        [DapperAttribute.Write(false)]
        public string cryptoDocClienteId
        {
            get { return BSI.GestDoc.Util.MD5Crypt.Criptografar(DocClienteId.ToString()); }
        }
        public Int64 UsuarioId { get; set; }
        public string DocClienteNomeArquivoSalvo { get; set; }
        public DateTime DocClienteDataUpload { get; set; }
        public int DocCliSituId { get; set; }
        public Int64 ClienteId { get; set; }
        public int DocCliTipoId { get; set; }
        public string DocClienteNomeArquivoOriginal { get; set; }
        public string DocClienteTipoArquivo { get; set; }

        [DapperAttribute.Write(false)]
        public DocumentoClienteSituacao DocumentoClienteSituacao { get; set; }

        [DapperAttribute.Write(false)]
        public DocumentoClienteTipo DocumentoClienteTipo { get; set; }
        
    }
}
