using System;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    public interface IDocumentoCliente
    {
        Int64 DocClienteId { get; set; }
        Int64 UsuarioId { get; set; }
        string DocClienteNomeArquivoSalvo { get; set; }
        DateTime DocClienteDataUpload { get; set; }
        int DocCliSituId { get; set; }
        Int64 ClienteId { get; set; }
        int DocCliTipoId { get; set; }
        string DocClienteNomeArquivoOriginal { get; set; }
        string DocClienteTipoArquivo { get; set; }

        DocumentoClienteSituacao DocumentoClienteSituacao { get; set; }

        DocumentoClienteTipo DocumentoClienteTipo { get; set; }

        string NomeArquivoSalvoAux { get; set; }
    }
}