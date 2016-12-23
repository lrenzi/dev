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
        /// 
        /// </summary>
        /// <param name="codTipoDocumento"></param>
        /// <returns></returns>
        public IEnumerable<DocumentoClienteTipo> ListarDocumentoTipoSituacao(string clienteId)
        {
            TipoDocumentoDal Dal = new TipoDocumentoDal();
            IEnumerable<DocumentoClienteTipo> listaTipoDocumento = null;

            //Recupera lista de Situacao     
            listaTipoDocumento = Dal.ListarTipoDocumento(clienteId);

            foreach (var tipoDocumento in listaTipoDocumento)
            {
                tipoDocumento.ListaSituacaoDocumentoCliente = this.ListarDocumentoSituacaoPorTipo(tipoDocumento.DocCliTipoId);
            }
            
            return listaTipoDocumento;
        }

        /// <summary>
        /// 
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
            IEnumerable<DocumentoClienteTipo> documentosTipoDocumentoCliente = this.ListarDocumentoTipoSituacao(usuarioId);
            
            //Recupera lista de DocumentosDados  pelo codigo do cliente logado   
            documentosClienteDados = Dal.ListarPropostas(usuarioId, clientId, numeroProposta);

            //Recupera lista de DocumentosCliente
            this.ConsultarInformacaoesDocumentosCliente(documentosClienteDados);

            //atribui o documentoCliente para o tipo de documento
            this.AtribuirTipoSituacaoParaDocumentoCliente(documentosTipoDocumentoCliente, documentosClienteDados);

            return documentosTipoDocumentoCliente.ToList();
        }


        private void AtribuirTipoSituacaoParaDocumentoCliente(IEnumerable<DocumentoClienteTipo> documentosTipo, IEnumerable<DocumentoClienteDados> listaDocumentoDados)
        {

            foreach (var tipo in documentosTipo)
            {
                foreach (var situacao in tipo.ListaSituacaoDocumentoCliente)
                {
                    foreach (var documentoDado in listaDocumentoDados)
                    {

                        IEnumerable<DocumentoCliente> documentoClienteRetorno = documentoDado.DocumentosCliente.ToList().Where(x => x.DocCliSituId == situacao.DocCliSituId && x.DocCliTipoId == tipo.DocCliTipoId);

                        if(documentoClienteRetorno.Count() > 0)
                        {
                            situacao.DocumentoCliente = (DocumentoCliente)documentoClienteRetorno.ToList()[0];
                        }

                    }
                }
            }

            foreach (var documentoDado in listaDocumentoDados)
            {

                

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

                    //Recupera lista de Situações por Tipo de DocumentoCliente
                    //this.ListarSituacaoPorTipoDocumentoCliente(retornoListaDocumentosCliente);
                    
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

        /// Lista de DocumentosDados
        /// </summary>
        /// <param name="listaDocumentosCliente"></param>
        //public void CarregarArquivoSalvoPorSituacaoDocumento(IEnumerable<DocumentoClienteDados> listaDocumentosClienteDados)
        //{
        //    PropostasDal Dal = new PropostasDal();

        //    foreach (var documentoClienteDado in listaDocumentosClienteDados)
        //    {
        //        List<DocumentoClienteTipo> documentosAux = documentoClienteDado.DocumentosCliente.ToList();


        //        foreach (var documentoCliente in documentoClienteDado.DocumentosCliente)
        //        {
        //            DocumentoClienteTipo tipo = documentoCliente.DocumentoClienteTipo;


        //            IEnumerable<DocumentoClienteTipo> tipoExistente = documentosAux.ToList().Where(x => x.DocCliTipoId == tipo.DocCliTipoId);


        //            if (tipoExistente.Count() == 0)
        //            {
        //                documentosAux.Add(tipo);
        //            }else
        //            {
        //                IEnumerable<DocumentoClienteSituacao> situacoesTipo = tipoExistente.ToList()[0].ListaSituacaoDocumentoCliente;
        //                situacoesTipo.ToList().Add()

        //            }
        //        }

        //        //consulta informações para documentos cliente
        //        IEnumerable<DocumentoCliente> retornoListaDocumentosCliente = Dal.ConsultarInfoDocumentoCliente(documentoClienteDado);

        //        if (retornoListaDocumentosCliente != null && retornoListaDocumentosCliente.Count() > 0)
        //        {
        //            documentoClienteDado.DocumentosCliente = retornoListaDocumentosCliente.ToList();

        //            //Recupera lista de Situações por Tipo de DocumentoCliente
        //            this.ListarSituacaoPorTipoDocumentoCliente(retornoListaDocumentosCliente);
        //        }
        //    }
        //}

    }
}
