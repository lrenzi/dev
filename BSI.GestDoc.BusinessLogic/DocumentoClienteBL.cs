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

        /// <summary>
        /// Lista de documentos Cliente
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="clientId"></param>
        /// <param name="numeroProposta"></param>
        /// <returns></returns>
        public List<DocumentoClienteDados> ListarDocumentosCliente(string usuarioId, string clientId, string numeroProposta)
        {
            PropostasDal Dal = new PropostasDal();
            IEnumerable<DocumentoClienteDados> documentosClienteDados = new  List<DocumentoClienteDados>();
            
            //Recupera lista de DocumentosDados     
            documentosClienteDados = Dal.ListarPropostas(usuarioId, clientId, numeroProposta);
            
            //Recupera lista de DocumentosCliente
            this.ConsultarInformacaoesDocumentosCliente(documentosClienteDados);
            
            return documentosClienteDados.ToList();
        }

        /// <summary>
        /// Lista de DocumentosDados
        /// </summary>
        /// <param name="listaDocumentosCliente"></param>
        public void ConsultarInformacaoesDocumentosCliente(IEnumerable<DocumentoClienteDados> listaDocumentosCliente)
        {
            PropostasDal Dal = new PropostasDal();

            foreach (var documentoClienteDado in listaDocumentosCliente)
            {
                //consulta informações para documentos cliente
                IEnumerable<DocumentoCliente> retornoListaDocumentosCliente = Dal.ConsultarInfoDocumentoCliente(documentoClienteDado);

                if (retornoListaDocumentosCliente != null && retornoListaDocumentosCliente.Count() > 0)
                {
                    documentoClienteDado.DocumentosCliente = retornoListaDocumentosCliente.ToList();

                    //Recupera lista de Situações por Tipo de DocumentoCliente
                    this.ListarSituacaoPorTipoDocumentoCliente(retornoListaDocumentosCliente);
                }
            }
        }

        /// <summary>
        /// Lista Situações por Tipo de Documento
        /// </summary>
        /// <param name="listaDocumentosCliente"></param>
        public void ListarSituacaoPorTipoDocumentoCliente(IEnumerable<DocumentoCliente> listaDocumentosCliente)
        {
            DocumentoClienteSituacaoDal Dal = new DocumentoClienteSituacaoDal();

            foreach (var documentoCliente in listaDocumentosCliente)
            {
                //busc lista de situações por tipo do documento
                IEnumerable<DocumentoClienteSituacao> retornoSituacaoDocumento = Dal.ListarSituacaoDocumentoCliente(documentoCliente.DocCliTipoId.ToString());

                if (retornoSituacaoDocumento != null && retornoSituacaoDocumento.Count() > 0)
                {
                    documentoCliente.DocumentoClienteTipo.ListaSituacaoDocumentoCliente = retornoSituacaoDocumento.ToList();

                    this.CarregarArquivoSalvoPorSituacaoDocumento(documentoCliente);
                }
            }
        }

        /// <summary>
        /// Verifica quais arquivos foram salvos para exibi-los na situação correspondente na página que informa as informações da proposta consultada
        /// </summary>
        /// <param name="listaDocumentosCliente"></param>
        public void CarregarArquivoSalvoPorSituacaoDocumento(DocumentoCliente documentoCliente)
        {
            //foreach (var documentoCliente in listaDocumentosCliente)
            //{
                //verifica se o documento corresponde a situacao existente na lista
                IEnumerable<DocumentoClienteSituacao> documentoRetorno = documentoCliente.DocumentoClienteTipo.ListaSituacaoDocumentoCliente.Where(x => x.DocCliSituId == documentoCliente.DocCliSituId);

                if(documentoRetorno.Count() > 0)
                {
                    //carrega o nome do arquivo salvo no campo auxiliar
                    documentoCliente.NomeArquivoSalvoAux = documentoCliente.DocClienteNomeArquivoOriginal;
                }

            //}

        }
    }
}
