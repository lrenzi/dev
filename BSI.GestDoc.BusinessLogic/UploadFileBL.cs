using BSI.GestDoc.CustomException.BusinessException;
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

        public string RecuperarCaminhoPastaDocumentosByClienteId(long clienteId_) {
            Cliente _cliente = new ClienteDal().GetCliente(clienteId_);
            if (_cliente == null)
                throw new BusinessException(BSI.GestDoc.Util.EnumTipoMensagem.Alerta, "Erro ao consultar o caminho do arquivo. Cliente não identificado.");
            if (string.IsNullOrEmpty(_cliente.ClientePastaDocumentos))
                throw new BusinessException(BSI.GestDoc.Util.EnumTipoMensagem.Alerta, "Erro ao consultar o caminho do arquivo. Caminho não cadastrado.");
            return _cliente.ClientePastaDocumentos;
        }

        public string RecuperarCaminhoPastaDocumentosByDocClienteId(long docClienteId_)
        {
            DocumentoCliente _documentoCliente = new DocumentoClienteDal().GetDocumentoCliente(docClienteId_);
            if (_documentoCliente == null)
                throw new BusinessException(BSI.GestDoc.Util.EnumTipoMensagem.Alerta, "Erro ao consultar o caminho do arquivo. Documento do Cliente não identificado.");
            return RecuperarCaminhoPastaDocumentosByClienteId(_documentoCliente.ClienteId);
        }

        public List<DocumentoClienteTipo> RetornarDocumentoClienteTipo(int clienteId_)
        {
            return new DocumentoClienteTipoDal().GetAllByIdCliente(clienteId_).ToList();
        }

        public virtual DocumentoCliente  EnviarDocumentoCliente(DocumentoCliente documentoCliente_)
        {
            throw new System.NotImplementedException("Método EnviarDocumentoCliente da classe UploadFileBL não implementado.");
        }

        public virtual DocumentoCliente RetornarArquivo(DocumentoCliente documentoCliente_)
        {
            return new DocumentoClienteDal().GetDocumentoCliente(documentoCliente_.DocClienteId);
        }

        
    }
}
