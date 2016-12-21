using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.DAL;
using System.Collections.Generic;
using System.Linq;

namespace BSI.GestDoc.BusinessLogic
{
    public class DocumentoClienteBL
    {
        public DocumentoClienteBL()
        {
        }

        public List<DocumentoClienteDados> ListarDocumentosCliente(string usuarioId, string clientId)
        {
            PropostasDal Dal = new PropostasDal();
            IEnumerable<DocumentoClienteDados> documentosClienteDados = new  List<DocumentoClienteDados>();
                 
            documentosClienteDados = Dal.ListarPropostas(usuarioId, clientId);
            
            this.ConsultarInformacaoesDocumentosCliente(documentosClienteDados);
            
            return documentosClienteDados.ToList();
        }

        public void ConsultarInformacaoesDocumentosCliente(IEnumerable<DocumentoClienteDados> listaDocumentosCliente)
        {
            PropostasDal Dal = new PropostasDal();

            foreach (var documentoClienteDado in listaDocumentosCliente)
            {
                IEnumerable<DocumentoCliente> retorno = Dal.ConsultarInfoDocumentoCliente(documentoClienteDado);

                if (retorno != null && retorno.Count() > 0)
                {
                    documentoClienteDado.DocumentosCliente = retorno.ToList();
                }
            }
        }
    }
}
