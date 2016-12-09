using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.BusinessLogic
{
    public class UploadFiles
    {
        public List<DocumentoClienteTipo> RetornarDocumentoClienteTipo(int ClienteId)
        {
            return new DocumentoClienteTipoDal().GetAllDocumentoClienteTipoByIdCliente(ClienteId).ToList();
        }
    }
}
