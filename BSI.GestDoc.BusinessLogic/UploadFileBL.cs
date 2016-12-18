using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.CRUD;
using System.Collections.Generic;
using System.Linq;

namespace BSI.GestDoc.BusinessLogic
{
    public class UploadFileBL
    {
        public UploadFileBL()
        {
        }

        public List<DocumentoClienteTipo> RetornarDocumentoClienteTipo(int clienteId_)
        {
            return new DocumentoClienteTipoDal().GetAllDocumentoClienteTipoByIdCliente(clienteId_).ToList();
        }

        public virtual DocumentoCliente  EnviarDocumentoCliente(DocumentoCliente documentoCliente_)
        {
            return documentoCliente_;
        }

        
    }
}
