using BSI.GestDoc.BusinessLogic.Util;
using BSI.GestDoc.CustomException.BusinessException;
using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository;
using BSI.GestDoc.Repository.CRUD;
using BSI.GestDoc.Util;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using static BSI.GestDoc.CustomException.BusinessException.BusinessException;

namespace BSI.GestDoc.BusinessLogic
{
    public class UploadFileBradescoBL : UploadFileBL
    {
        public UploadFileBradescoBL() : base()
        {
        }



        public override DocumentoCliente EnviarDocumentoCliente(DocumentoCliente documentoCliente_)
        {
            UtilFileBradesco utilFileBradesco = new UtilFileBradesco();

            //Validações **

            #region 1 - Verifica tipo/ versão pdf
            if (UtilFile.GetMIMEType(documentoCliente_.DocClienteNomeArquivoOriginal).ToLower() != "application/pdf")
            {
                throw new Exception("Erro - Documento deve ser do tipo PDF");
            }
            #endregion

            #region 2 - Verifica número proposta no PDF
            DocumentoClienteDados _documentoClienteDados = null;
            try
            {
                _documentoClienteDados = utilFileBradesco.LerPdf(WorkingFolder + "\\" + documentoCliente_.DocClienteNomeArquivoSalvo);
                Int64 _valor;
                if (!Int64.TryParse(_documentoClienteDados.DocCliDadosValor, out _valor))
                {
                    throw new Exception("Valor do campo contrato deve ser numérico.");
                }
                _documentoClienteDados.ClienteId = documentoCliente_.ClienteId;
                _documentoClienteDados.TipoInfoCliId = (new ClienteTipoInformacaoClienteDal().GetAllByIdCliente(_documentoClienteDados.ClienteId).First()).TipoInfoCliId;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar o número do contrato. Erro [" + ex.Message + "]");
            }
            #endregion

            #region Validar Conteúdo PDF

            validarConteudoPDF(documentoCliente_);

            #endregion

            #region Processamentos

            Processamentos(documentoCliente_);

            #endregion

            #region 3 - Consultar numero proposta por usuário na base
            //Não pode inserir um numero de proposta na base sem antes verificar se o numero da proposta já existe na base e se a proposta foi cadastrada pelo mesmo usuário que está enviando o arquivo.

            //Recupera todos os documentos de clientes que possuem o mesmo número de proposta
            List<DocumentoCliente> _documentosClienteCadastrado = new EnviarArquivoDal().ConsultarNumeroPropostaPorUsuario(_documentoClienteDados.DocCliDadosValor.Trim()).ToList();
            if (_documentosClienteCadastrado.FindAll(p => p.UsuarioId != documentoCliente_.UsuarioId).Count > 0)
            {
                throw new BusinessException(0, EnumTipoMensagem.Alerta, "Proposta enviada por outro usuário.");
            }
            List<DocumentoClienteSituacao> _documentosClienteSituacao = new DocumentoClienteSituacaoDal().GetAllDocumentoClienteSituacaoByDocCliTipoId(documentoCliente_.DocCliTipoId).ToList();

            if (_documentosClienteSituacao.Count == 0)
                throw new Exception("Situação não cadastrada para este Tipo de Documento.");
            documentoCliente_.DocCliSituId = _documentosClienteSituacao.Min(p => p.DocCliSituId);

            #endregion

            #region 5 - Consulta arquivos já existentes para o numero de proposta
            //Caso já exista um tipo e situação do arquivo na base igual ao que o usuário está tentando realizar o upload, irá perguntar ao usuário se ele deseja sobreescrever.
            if (!Reenvio && _documentosClienteCadastrado.FindAll(p => p.DocCliSituId == documentoCliente_.DocCliSituId).Count > 0)
            {
                throw new BusinessException(EnumTipoMensagem.Pergunta, "Proposta já cadastrada para este tipo de arquivo e situação. Deseja Reenviar?");
            }
            #endregion

            #region 4 - Insere o numero proposta em base
            //Se o numero da proposta está OK e ainda não existe na base, irá realizar o insert na base, caso já exista irá utilizar o ID do numero de proposta que já existe na base
            List<DocumentoClienteDados> _documentosClienteDados = new DocumentoClienteDadosDal().GetAllByDocCliDadosValor(_documentoClienteDados.DocCliDadosValor).ToList();
            if (_documentosClienteDados.Count == 0)
            {
                _documentoClienteDados = new EnviarArquivoDal().InserirDocumentoClienteDados(_documentoClienteDados);
            }
            else
            {
                _documentoClienteDados = _documentosClienteDados.First();
            }
            #endregion

            #region 6 - Salva arquivo em servidor e faz insert na base

            List<DocumentoCliente> _documentosClientes = new EnviarArquivoDal().ConsultarDocumentoClientePorDocCliDadosValorDocCliTipoId(
                _documentoClienteDados.DocCliDadosValor,
                documentoCliente_.DocCliTipoId).ToList();

            if (_documentosClientes.Count > 0)
            {
                //Apaga o arquivo
                if (System.IO.File.Exists(WorkingFolder + "\\" + _documentosClientes.First().DocClienteNomeArquivoSalvo))
                    System.IO.File.Delete(WorkingFolder + "\\" + _documentosClientes.First().DocClienteNomeArquivoSalvo);
                //Atualiza
                documentoCliente_.DocClienteId = _documentosClientes.First().DocClienteId;
                new DocumentoClienteDal().Update(documentoCliente_);
            }
            else //Insere
            {
                documentoCliente_ = (DocumentoCliente)new DocumentoClienteDal().Insert(documentoCliente_);
            }
            #endregion

            #region 7 - Faz insert na base p / relacionar id do documento e do num.de proposta

            if (_documentosClientes.Count == 0)
            {
                DocumentoClienteDadosDoc _documentoClienteDadosDoc = new DocumentoClienteDadosDoc();
                _documentoClienteDadosDoc.DocClienteId = documentoCliente_.DocClienteId;
                _documentoClienteDadosDoc.DocCliDadosId = _documentoClienteDados.DocCliDadosId;
                _documentoClienteDadosDoc = new DocumentoClienteDadosDocDal().Insert(_documentoClienteDadosDoc);
            }

            #endregion

            return documentoCliente_;
        }

        private void Processamentos(DocumentoCliente documentoCliente_)
        {
            //Obtem pasta de destino do arquivo de configuracao
            string _pastaArquivoAssinatura = ConfigurationManager.AppSettings["Bradesco.PastaArquivoAssinatura"];
            string _arquivoAssinatura = ConfigurationManager.AppSettings["Bradesco.ArquivoAssinatura"];

            switch (documentoCliente_.DocCliTipoId)
            {
                case 1:
                    //Valida CCB
                    processamentoCCB(documentoCliente_, _pastaArquivoAssinatura, _arquivoAssinatura);
                    break;
                case 2:
                    //Valida CET
                    processamentoCET(documentoCliente_);

                    break;
                case 3:
                    //Valida CET
                    processamentoSeguro(documentoCliente_);
                    break;
            }
        }

        private void processamentoCCB(DocumentoCliente documentoCliente_, string pastaArquivoAssinatura_, string arquivoAssinatura_)
        {
            UtilFileBradesco utilBradesco = new Util.UtilFileBradesco();
            string fileArquivoUpload = WorkingFolder + "\\" + documentoCliente_.DocClienteNomeArquivoSalvo;
            string fileArquivoAssinatura = WorkingFolder + "\\" + pastaArquivoAssinatura_ + "\\" + arquivoAssinatura_;
            string[] Arquivos = { fileArquivoUpload, fileArquivoAssinatura };

            try
            {

                string validaCabecalho = ConfigurationManager.AppSettings["Bradesco.ValidaCabecalho_CCB"].ToString();
                string diretorioBalde = ConfigurationManager.AppSettings["Bradesco.DiretorioBalde"];
                string fileAux = WorkingFolder + "\\" + diretorioBalde + "\\" + documentoCliente_.DocClienteNomeArquivoSalvo + "_aux";
                string fileAuxAssinatura = WorkingFolder + "\\" + diretorioBalde + "\\" + documentoCliente_.DocClienteNomeArquivoSalvo;

                if (VerificarCabecalhoContratoCCB(fileArquivoUpload, validaCabecalho)) //Contrato CCB
                {
                    //Le os dados do Pdf e cria o novo com o modelo
                    utilBradesco.EscreverPdf(WorkingFolder, Arquivos[0], Arquivos[1]);

                    File.Copy(Arquivos[0], fileAux);

                    Arquivos[0] = fileAux;
                    Arquivos[1] = fileAuxAssinatura;

                    //Junta os dois arquivos em um só
                    UtilFileBradesco.MergePDFs(Arquivos, fileArquivoUpload);


                }
            }
            finally
            {
                foreach (string file in Arquivos)
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
            }

        }

        private void processamentoCET(DocumentoCliente documentoCliente_) { }
        private void processamentoSeguro(DocumentoCliente documentoCliente_) { }

        private void validarConteudoPDF(DocumentoCliente documentoCliente_)
        {
            #region Valida conteudo dos PDFs

            Boolean blnArquivoPDFInvalido = false;
            StringBuilder sbPDFInvalido = new StringBuilder("Conteudo inválido para o(s) arquivo(s):" + "\n");

            string valida = string.Empty;

            switch (documentoCliente_.DocCliTipoId)
            {
                case 1:
                    //Valida CCB
                    valida = ConfigurationManager.AppSettings["Bradesco.Valida_CCB"].ToString().ToUpper().Trim();
                    if (!VerificarContrato(documentoCliente_.DocClienteNomeArquivoSalvo, valida))
                    {
                        sbPDFInvalido.Append("CCB: " + documentoCliente_.DocClienteNomeArquivoOriginal + "\n");
                        blnArquivoPDFInvalido = true;
                    }
                    break;
                case 2:
                    //Valida CET
                    valida = ConfigurationManager.AppSettings["Bradesco.Valida_CET"].ToString().ToUpper().Trim();

                    if (!VerificarContrato(documentoCliente_.DocClienteNomeArquivoSalvo, valida))
                    {
                        sbPDFInvalido.Append("CCT: " + documentoCliente_.DocClienteNomeArquivoOriginal + "\n");
                        blnArquivoPDFInvalido = true;
                    }
                    break;
                case 3:
                    //Valida CET
                    valida = ConfigurationManager.AppSettings["Bradesco.Valida_Seguro"].ToString().ToUpper().Trim();

                    if (!VerificarContrato(documentoCliente_.DocClienteNomeArquivoSalvo, valida))
                    {
                        sbPDFInvalido.Append("Seguro: " + documentoCliente_.DocClienteNomeArquivoOriginal + "\n");
                        blnArquivoPDFInvalido = true;
                    }
                    break;
            }


            if (blnArquivoPDFInvalido)
            {
                throw new BusinessException(EnumTipoMensagem.Alerta, sbPDFInvalido.ToString());
            }

            #endregion

        }
        private bool VerificarContrato(string docClienteNomeArquivoSalvo_, string conteudo_)
        {
            bool retorno = false;
            using (PdfReader pdfReader = new PdfReader(WorkingFolder + "\\" + docClienteNomeArquivoSalvo_))
            {
                retorno = PdfTextExtractor.GetTextFromPage(pdfReader, 1, new SimpleTextExtractionStrategy()).Substring(0, 300).ToUpper().Contains(conteudo_);
                pdfReader.Close();
                pdfReader.Dispose();
            }

            return retorno;
        }

        /// <summary>
        /// Verifica se o contrato é CCB
        /// </summary>
        /// <param name="Caminho">Caminho do contrato</param>
        /// <returns>Verdadeiro ou Falso</returns>
        public bool VerificarCabecalhoContratoCCB(string caminho_, string conteudo_)
        {
            bool retorno = false;
            using (PdfReader pdfReader = new PdfReader(caminho_))
            {
                retorno = PdfTextExtractor.GetTextFromPage(pdfReader, 1, new SimpleTextExtractionStrategy()).Substring(0, 62).ToUpper().Contains(conteudo_.ToUpper().Trim());
                pdfReader.Close();
                pdfReader.Dispose();
            }

            return retorno;
        }
    }
}
