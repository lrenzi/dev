using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Xml.Linq;
using System.Xml;
using iTextSharp.text.pdf;
using iTextSharp.text.xml.xmp;
using iTextSharp.text.pdf.parser;
using System.util;
using System.Globalization;
using System.Collections;
using iTextSharp.text;
using Ionic.Zip;
using BSI.GestDoc.Entity;

namespace BSI.GestDoc.BusinessLogic.Util
{
    public class UtilFileBradesco
    {
        /// <summary>
        /// Lê o arquivo pdf para extrair os dados
        /// </summary>
        /// <param name="Caminho">Caminho do arquivo PDF a ser lido</param>
        public DocumentoClienteDados LerPdf(string Caminho)
        {
            DocumentoClienteDados dadosCliente = new DocumentoClienteDados();
            string NomeArquivo = new FileInfo(Caminho).Name;

            dadosCliente.DocCliDadosValor = RetornarValor(Caminho, ConfigurationManager.AppSettings["Bradesco.PosicaoContrato"].ToString().Split(','), int.Parse(ConfigurationManager.AppSettings["Bradesco.PaginaContrato"].ToString()));
            //Preenche o modelo criando um novo arquivo
            return dadosCliente;
        }

        /// <summary>
        /// Lê o arquivo pdf para extrair os dados
        /// </summary>
        /// <param name="Caminho">Caminho do arquivo PDF a ser lido</param>
        public void EscreverPdf(string workFolder_, string caminhoCompletoArquivoSalvo_, string caminhoCompletoArquivoAssinatura_)
        {
            string NomeArquivo = new FileInfo(caminhoCompletoArquivoSalvo_).Name;

            string contrato = RetornarValor(caminhoCompletoArquivoSalvo_, ConfigurationManager.AppSettings["Bradesco.PosicaoContrato"].ToString().Split(','), int.Parse(ConfigurationManager.AppSettings["Bradesco.PaginaContrato"].ToString()));
            string CPF = RetornarValor(caminhoCompletoArquivoSalvo_, ConfigurationManager.AppSettings["Bradesco.CPF_MFCCB"].ToString().Split(','), int.Parse(ConfigurationManager.AppSettings["Bradesco.PaginaMunicipio"].ToString()));
            string municipio = string.Empty;

            //verifica se é pessoa fisica ou juridica
            if (CPF.Length <= 14)
                municipio = ToTitleCase(RetornarValor(caminhoCompletoArquivoSalvo_, ConfigurationManager.AppSettings["Bradesco.CidadeCCB"].ToString().Split(','), int.Parse(ConfigurationManager.AppSettings["Bradesco.PaginaMunicipio"].ToString())));
            else
                municipio = ToTitleCase(RetornarValor(caminhoCompletoArquivoSalvo_, ConfigurationManager.AppSettings["Bradesco.CidadePj"].ToString().Split(','), int.Parse(ConfigurationManager.AppSettings["Bradesco.PaginaMunicipio"].ToString())));

            //Preenche o modelo criando um novo arquivo
            PreencherModelo(workFolder_, caminhoCompletoArquivoAssinatura_, contrato, CPF, municipio, NomeArquivo);
        }



        /// <summary>
        /// A partir do documento de modelo preenche os dados lidos
        /// </summary>
        /// <param name="Valores">Valores a serem preenchidos</param>
        /// <param name="NomeArquivo">Nome do arquivo a Ser gerado</param>
        private void PreencherModelo(string workFolder_, string caminhoCompletoArquivoAssinatura_, string contrato_, string CPF_, string municipio_, string NomeArquivo)
        {
            using (Stream newpdfStream = new FileStream(System.IO.Path.Combine(workFolder_ + "\\" + ConfigurationManager.AppSettings["Bradesco.DiretorioBalde"], NomeArquivo), FileMode.Create, FileAccess.ReadWrite))
            {
                PdfReader pdfReader = new PdfReader(caminhoCompletoArquivoAssinatura_);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, newpdfStream);

                AcroFields acroFields = pdfStamper.AcroFields;

                acroFields.SetField("contrato", contrato_);
                acroFields.SetField("Cidade", municipio_);
                acroFields.SetField("Dia", DateTime.Now.ToString("dd"));
                acroFields.SetField("Ano", DateTime.Now.Year.ToString().Substring(3, 1));
                acroFields.SetField("Mes", ToTitleCase(new DateTime(1900, DateTime.Now.Month, 1).ToString("MMMM", new CultureInfo("pt-BR"))));

                //marcando os campos como somente leitura
                acroFields.SetFieldProperty("contrato", "setfflags", PdfFormField.FF_READ_ONLY, null);
                acroFields.SetFieldProperty("Cidade", "setfflags", PdfFormField.FF_READ_ONLY, null);
                acroFields.SetFieldProperty("Dia", "setfflags", PdfFormField.FF_READ_ONLY, null);
                acroFields.SetFieldProperty("Ano", "setfflags", PdfFormField.FF_READ_ONLY, null);
                acroFields.SetFieldProperty("Mes", "setfflags", PdfFormField.FF_READ_ONLY, null);

                acroFields = null;
                pdfStamper.Close();
                pdfReader.Close();
                pdfReader.Dispose();
                pdfStamper.Dispose();
                newpdfStream.Close();
                newpdfStream.Dispose();
            }
        }

        /// <summary>
        /// Função para juntar dois ou mais arquivos em apenas um
        /// </summary>
        /// <param name="fileNames">Arquivo(s) que farão parte do merge</param>
        /// <param name="targetPdf">Nome do arquivo de destino</param>
        public static bool MergePDFs(string[] fileNames, string targetPdf)
        {
            bool merged = true;
            using (FileStream stream = new FileStream(targetPdf, FileMode.Create))
            {
                Document document = new Document();
                PdfCopy pdf = new PdfCopy(document, stream);
                PdfReader reader = null;
                try
                {
                    document.Open();
                    foreach (string file in fileNames)
                    {
                        reader = new PdfReader(file);
                        pdf.AddDocument(reader);
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    merged = false;
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                finally
                {
                    if (document != null)
                    {
                        document.Close();
                    }
                }
            }
            return merged;
        }


        /// <summary>
        /// Retorna os campos do PDF por posição faz um retangulo 
        /// </summary>
        /// <param name="CaminhoArquivo">Caminho do arqivo a ser lido</param>
        /// <param name="Posicoes">Array com 3 posições</param>
        /// <param name="Pagina">Número da página aonde se encontra os dados do cliente</param>
        /// <returns>Retorna o valor encontrado</returns>
        private string RetornarValor(String CaminhoArquivo, string[] Posicoes, int Pagina)
        {
            string retorno = string.Empty;

            using (PdfReader pdfReader = new PdfReader(CaminhoArquivo))
            {
                RectangleJ rect = new RectangleJ(float.Parse(Posicoes[0]), float.Parse(Posicoes[1]), float.Parse(Posicoes[2]), float.Parse(Posicoes[3]));
                RenderFilter[] renderFilter = { new RegionTextRenderFilter(rect) };

                ITextExtractionStrategy textExtractionStrategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), renderFilter);
                retorno = PdfTextExtractor.GetTextFromPage(pdfReader, Pagina, textExtractionStrategy);

                rect = null;
                renderFilter = null;
                textExtractionStrategy = null;

                pdfReader.Close();
                pdfReader.Dispose();
            }
            
            return retorno == string.Empty ? " " : retorno;
        }

        /// <summary>
        /// Formata string com primeiro caractere em maiúsculo e os demais em minúsculo
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string ToTitleCase(string texto)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(texto.ToLower());
        }


    }
}
