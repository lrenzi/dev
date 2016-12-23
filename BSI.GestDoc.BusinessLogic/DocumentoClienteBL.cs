using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.DAL;
using System.Collections.Generic;
using System.Linq;

namespace BSI.GestDoc.BusinessLogic
{
    public class DocumentoClienteBL
    {
        #region construtor
        public DocumentoClienteBL()
        {
        }
        #endregion

        #region metodos
        /// <summary>
        /// Lista de documentos Cliente
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="clientId"></param>
        /// <param name="numeroProposta"></param>
        /// <returns></returns>
        public List<DocumentoClienteTipo> ListarDocumentosCliente(string usuarioId, string clientId, string numeroProposta)
        {
            PropostasDal Dal = new PropostasDal();
            IEnumerable<DocumentoClienteDados> documentosClienteDados = new List<DocumentoClienteDados>();

            //recupera os tipos, e situações de cada tipo de documentos, do cliente logado
            IEnumerable<DocumentoClienteTipo> listaDocumentosTipo = this.ListarDocumentoTipoSituacao(usuarioId);

            //Recupera lista de DocumentosDados  pelo codigo do cliente logado   
            documentosClienteDados = Dal.ListarPropostas(usuarioId, clientId, numeroProposta);

            //Recupera lista de DocumentosCliente
            this.ConsultarInformacaoesDocumentosCliente(documentosClienteDados);

            //Associa o documentoCliente para o tipo de documento
            this.AssociarTipoSituacaoParaDocumentoCliente(listaDocumentosTipo, documentosClienteDados);

            //Associa o tipo de documento correspondente ao documento dados consultado pelo CliDocID
            List<DocumentoClienteTipo> tiposDocumentoCliente = this.AssociarTipoDocumentoCliente(listaDocumentosTipo, documentosClienteDados);

            return tiposDocumentoCliente.ToList();
        }

        /// <summary>
        /// Lista os tipos de situação para o cliente
        /// </summary>
        /// <param name="codTipoDocumento"></param>
        /// <returns></returns>
        public IEnumerable<DocumentoClienteTipo> ListarDocumentoTipoSituacao(string clienteId)
        {
            TipoDocumentoDal Dal = new TipoDocumentoDal();
            IEnumerable<DocumentoClienteTipo> listaTipoDocumento = null;

            //Recupera lista de Tipo     
            listaTipoDocumento = Dal.ListarTipoDocumento(clienteId);

            foreach (var tipoDocumento in listaTipoDocumento)
            {
                tipoDocumento.ListaSituacaoDocumentoCliente = this.ListarDocumentoSituacaoPorTipo(tipoDocumento.DocCliTipoId);

            }
            return listaTipoDocumento;
        }

        /// <summary>
        /// Lista as situações por tipo
        /// </summary>
        /// <param name="codTipoDocumento"></param>
        /// <returns></returns>
        public IEnumerable<DocumentoClienteSituacao> ListarDocumentoSituacaoPorTipo(int codTipoDocumento)
        {
            DocumentoClienteSituacaoDal Dal = new DocumentoClienteSituacaoDal();
            IEnumerable<DocumentoClienteSituacao> listaSituacaoDocumento = null;

            //Recupera lista de Situacao     
            listaSituacaoDocumento = Dal.ListarSituacaoDocumentoCliente(codTipoDocumento);

            return listaSituacaoDocumento;
        }

        /// <summary>
        /// Associa o tipo de documento correspondente ao documento dados consultado pelo CliDocID
        /// </summary>
        /// <param name="listaDocumentoDados"></param>
        /// <param name="listaDocumentosTipo"></param>
        private List<DocumentoClienteTipo> AssociarTipoDocumentoCliente(IEnumerable<DocumentoClienteTipo> listaDocumentosTipo, IEnumerable<DocumentoClienteDados> listaDocumentoDados)
        {
            List<DocumentoClienteTipo> tiposCorrespondentes = new List<DocumentoClienteTipo>();

            //itera lista de documentos dados
            foreach (var documentoDados in listaDocumentoDados)
            {
                //itera lista de documentos cliente
                foreach (var documentoCliente in documentoDados.DocumentosCliente)
                {
                    //verifica se o tipo documento corresponde ao item da lista de tipos do cliente
                    IEnumerable<DocumentoClienteTipo> tipoCorrespondente = listaDocumentosTipo.ToList().Where(x => x.DocCliTipoId == documentoCliente.DocCliTipoId);
                    
                    if(tipoCorrespondente.Count() > 0)
                    {
                        //recupera o documento tipo correspondente
                        IEnumerable<DocumentoClienteTipo> tipoExistente = tiposCorrespondentes.ToList().Where(x => x.DocCliTipoId == documentoCliente.DocCliTipoId);

                        //verifica se o tipo da lista já foi recupera e adiciona na lista de tipos correspondentes ao documento
                        if (tipoExistente.Count() == 0)
                        {
                            tiposCorrespondentes.Add((DocumentoClienteTipo)tipoCorrespondente.ToList()[0]);
                        }
                    }
                }
            }

            return tiposCorrespondentes;
        }

        /// <summary>
        /// Associa a cada situação a documento cliente
        /// </summary>
        /// <param name="documentosTipo"></param>
        /// <param name="listaDocumentoDados"></param>
        private void AssociarTipoSituacaoParaDocumentoCliente(IEnumerable<DocumentoClienteTipo> listaDocumentosTipo, IEnumerable<DocumentoClienteDados> listaDocumentoDados)
        {
            foreach (var tipo in listaDocumentosTipo)
            {
                foreach (var situacao in tipo.ListaSituacaoDocumentoCliente)
                {
                    foreach (var documentoDado in listaDocumentoDados)
                    {
                        IEnumerable<DocumentoCliente> documentoClienteRetorno = documentoDado.DocumentosCliente.ToList().Where(x => x.DocCliSituId == situacao.DocCliSituId && x.DocCliTipoId == tipo.DocCliTipoId);

                        if (documentoClienteRetorno.Count() > 0)
                        {
                            situacao.DocumentoCliente = (DocumentoCliente)documentoClienteRetorno.ToList()[0];                            
                        }
                    }
                }
            }
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
                IEnumerable<DocumentoClienteSituacao> retornoSituacaoDocumento = Dal.ListarSituacaoDocumentoCliente(documentoCliente.DocCliTipoId);

                if (retornoSituacaoDocumento != null && retornoSituacaoDocumento.Count() > 0)
                {
                    documentoCliente.DocumentoClienteTipo.ListaSituacaoDocumentoCliente = retornoSituacaoDocumento.ToList();

                    this.CarregarArquivoSalvoPorSituacaoDocumento(documentoCliente);
                }
            }
        }

        // <summary>
        // Verifica quais arquivos foram salvos para exibi-los na situação correspondente na página que informa as informações da proposta consultada
        // </summary>
        // <param name = "listaDocumentosCliente" ></ param >
        public void CarregarArquivoSalvoPorSituacaoDocumento(DocumentoCliente documentoCliente)
        {
            //verifica se o documento corresponde a situacao existente na lista
            IEnumerable<DocumentoClienteSituacao> documentoRetorno = documentoCliente.DocumentoClienteTipo.ListaSituacaoDocumentoCliente.Where(x => x.DocCliSituId == documentoCliente.DocCliSituId && x.DocCliTipoId == documentoCliente.DocCliTipoId);

            if (documentoRetorno.Count() > 0)
            {
                //carrega o nome do arquivo salvo no campo auxiliar
                documentoRetorno.ToList()[0].NomeArquivoSalvoAux = documentoCliente.DocClienteNomeArquivoOriginal;
            }
        }
    }
    #endregion
}
