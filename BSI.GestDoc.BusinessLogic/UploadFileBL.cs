using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.CRUD;
using System.Collections.Generic;
using System.Linq;

namespace BSI.GestDoc.BusinessLogic
{
    public class UploadFileBL
    {
        public bool Reenvio { get; set; }
        public string WorkingFolder { get; set; }

        public UploadFileBL()
        {
        }

        public List<DocumentoClienteTipo> RetornarDocumentoClienteTipo(int clienteId_)
        {
            return new DocumentoClienteTipoDal().GetAllDocumentoClienteTipoByIdCliente(clienteId_).ToList();
        }

        public virtual DocumentoCliente  EnviarDocumentoCliente(DocumentoCliente documentoCliente_)
        {
            throw new System.NotImplementedException("Método EnviarDocumentoCliente da classe UploadFileBL não implementado.");
        }

        public virtual DocumentoCliente RetornarArquivo(DocumentoCliente documentoCliente_)
        {
            return new DocumentoClienteDal().GetDocumentoClienteByDocClienteId(documentoCliente_.DocClienteId);
        }

        
    }
}
