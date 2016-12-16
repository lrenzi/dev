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

namespace BSI.GestDoc.UtilFile
{
    public class UtilFile
    {

        /// <summary>
        /// Le todos os arquivos no diretório de entrada para fazer a junção e preencher o modelo
        /// </summary>
        /// <returns>Verdadeiro ou falso</returns>
        public bool LerDiretorio()
        {
            List<string> ListArqDel = new List<string>();
            List<string> ListArqMove = new List<string>();

            bool bRetorno = false;
            try
            {
                if (!Directory.Exists(ConfigurationManager.AppSettings["DiretorioJuncao"]))
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["DiretorioJuncao"]);

                //Varre o diretorio para junção dos pdf´s
                foreach (string c in Directory.GetFiles(ConfigurationManager.AppSettings["DiretorioInicio"], "*.PDF"))
                {
                    //mover todos os arquivos do balde para um diretório de backup
                    this.MoverArquivosBaldeBackup();

                    if (VerificarContrato(c.ToString())) //Contrato CCB
                    {
                        //Le os dados do Pdf e cria o novo com o modelo
                        LerPdf(c);

                        string CaminhoDestino = System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioJuncao"], new FileInfo(c).Name);
                        string[] Arquivos = new string[] { System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioBalde"], new FileInfo(c).Name), c };

                        //Junta os dois arquivos em um só
                        MergePDFs(Arquivos, CaminhoDestino);

                        //Exclui o arquivo caso ele já exista no diretorio de destino
                        if (File.Exists(System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioBalde"], new FileInfo(c).Name)))
                            File.Delete(System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioBalde"], new FileInfo(c).Name));

                        //move o arquivo para o diretorio balde
                        File.Move(CaminhoDestino, System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioBalde"], new FileInfo(c).Name));

                        //Exclui o arquivo original
                        File.Delete(c);
                    }
                    else //outros tipos de contrato devem ser apenas movidos
                    {
                        File.Copy(c, System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioJuncao"], new FileInfo(c).Name),true);
                        File.Delete(c);
                        File.Copy(System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioJuncao"], new FileInfo(c).Name), System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioBalde"], new FileInfo(c).Name), true);
                        File.Delete(System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioJuncao"], new FileInfo(c).Name));
                    }
                };

                bRetorno = true;
            }
            catch (Exception ex)
            {
                bRetorno = false;
                throw ex;
            }
            return bRetorno;
        }

        public bool CriarArquivoZip(ref string NomeArquivoZip)
        {
            bool bRetorno = false;
            string NmArquivo = string.Empty;
            try
            {
                List<string> Arquivos = new List<string>(Directory.GetFiles(ConfigurationManager.AppSettings["DiretorioEmail"], "*.PDF").ToList());

                NmArquivo = string.Format("{0}{1}", DateTime.Now.ToString("dd_MM_yyyy_HH_mm"), ConfigurationManager.AppSettings["ExtensaoZip"]);
                CompactarArquivos(Arquivos, NmArquivo);
                NomeArquivoZip = System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioEmail"], NmArquivo);

                Arquivos.ForEach(c =>
                {
                    if (File.Exists(c))
                        File.Delete(c);
                });

                bRetorno = true;
            }
            catch (Exception ex)
            {
                NomeArquivoZip = "";
                throw ex;
            }


            return bRetorno;
        }

        public bool EfetuarBackup()
        {

            try
            {
                if (!Directory.Exists(ConfigurationManager.AppSettings["DiretorioBKP"]))
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["DiretorioBKP"]);

                foreach (string c in Directory.GetFiles(ConfigurationManager.AppSettings["DiretorioEmail"], "*.PDF"))
                {
                    bool existe = false;

                    //Verifica se existe arquivo com o mesmo nome no diretório DiretorioBalde
                    foreach (string b in Directory.GetFiles(ConfigurationManager.AppSettings["DiretorioBalde"], "*.PDF"))
                    {
                        if (System.IO.Path.GetFileNameWithoutExtension(b).Contains(System.IO.Path.GetFileNameWithoutExtension(c)))
                        {
                            existe = true;
                            break;
                        }
                    }

                    //Caso o mesmo não exista, o arquivo do diretório OUT deve ser copiado para o backup
                    if(!existe)
                        File.Copy(c, System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioBKP"], new FileInfo(c).Name), true);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }
        
        /// <summary>
        /// Verifica se o contrato é CCB
        /// </summary>
        /// <param name="Caminho">Caminho do contrato</param>
        /// <returns>Verdadeiro ou Falso</returns>
        private bool VerificarContrato(string Caminho)
        {
            bool retorno = false;
            using (PdfReader pdfReader = new PdfReader(Caminho))
            {
                retorno = PdfTextExtractor.GetTextFromPage(pdfReader, 1, new SimpleTextExtractionStrategy()).Substring(0, 62).ToUpper().Contains(ConfigurationManager.AppSettings["Cabecalho"].ToString().ToUpper().Trim());
                pdfReader.Close();
                pdfReader.Dispose();
            }

            return retorno;
        }


        /// <summary>
        /// Lê o arquivo pdf para extrair os dados
        /// </summary>
        /// <param name="Caminho">Caminho do arquivo PDF a ser lido</param>
        private void LerPdf(string Caminho)
        {
            DocumentoClienteDados dadosCliente = new DocumentoClienteDados();
            string NomeArquivo = new FileInfo(Caminho).Name;

            dadosCliente.DocCliDadosValor = RetornarValor(Caminho, ConfigurationManager.AppSettings["Contrato"].ToString().Split(','), int.Parse(ConfigurationManager.AppSettings["PaginaContrato"].ToString()));
            //cliente.CPF = RetornarValor(Caminho, ConfigurationManager.AppSettings["CPF_MFCCB"].ToString().Split(','), int.Parse(ConfigurationManager.AppSettings["PaginaMunicipio"].ToString()));

            ////verifica se é pessoa fisica ou juridica
            //if (cliente.CPF.Length <= 14)
            //    cliente.Municipio = ToTitleCase(RetornarValor(Caminho, ConfigurationManager.AppSettings["CidadeCCB"].ToString().Split(','), int.Parse(ConfigurationManager.AppSettings["PaginaMunicipio"].ToString())));
            //else
            //    cliente.Municipio = ToTitleCase(RetornarValor(Caminho, ConfigurationManager.AppSettings["CidadePj"].ToString().Split(','), int.Parse(ConfigurationManager.AppSettings["PaginaMunicipio"].ToString())));
            
            //Preenche o modelo criando um novo arquivo
            PreencherModelo(dadosCliente, NomeArquivo);
        }

        /// <summary>
        /// Move arquivos do balde para backup
        /// </summary>
        private void MoverArquivosBaldeBackup()
        {
            
            if (!Directory.Exists(ConfigurationManager.AppSettings["DiretorioBaldeBKP"]))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["DiretorioBaldeBKP"]);
                        
            #region Mover para Backup
            foreach (string c in Directory.GetFiles(ConfigurationManager.AppSettings["DiretorioBalde"]))
            {
                //renomear o arquivo antes de jogar na pasta de backup para evitar duplicidades
                string novoNome = System.IO.Path.GetFileName(c).Replace(System.IO.Path.GetFileNameWithoutExtension(c), System.IO.Path.GetFileNameWithoutExtension(c) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));

                //move o arquivo para pasta de backup
                File.Move(c, System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioBaldeBKP"], novoNome));
            }
            #endregion

            #region Move diretórios
            foreach (string dir in Directory.GetDirectories(ConfigurationManager.AppSettings["DiretorioBalde"]))
            {
                //renomear o diretório antes de jogar na pasta de backup para evitar duplicidades
                string novoNome = new DirectoryInfo(dir).Name + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");

                //move o diretório para pasta de backup
                Directory.Move(dir, System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioBaldeBKP"], novoNome));
            }
            #endregion
        }

        /// <summary>
        /// A partir do documento de modelo preenche os dados lidos
        /// </summary>
        /// <param name="Valores">Valores a serem preenchidos</param>
        /// <param name="NomeArquivo">Nome do arquivo a Ser gerado</param>
        private void PreencherModelo(DocumentoClienteDados Valores, string NomeArquivo)
        {
            if (!Directory.Exists(ConfigurationManager.AppSettings["DiretorioBalde"]))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["DiretorioBalde"]);
            
            using (Stream newpdfStream = new FileStream(System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioBalde"], NomeArquivo), FileMode.Create, FileAccess.ReadWrite))
            {
                PdfReader pdfReader = new PdfReader(ConfigurationManager.AppSettings["ArquivoModelo"]);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, newpdfStream);

                AcroFields acroFields = pdfStamper.AcroFields;

                acroFields.SetField("contrato", Valores.DocCliDadosValor);
                //acroFields.SetField("Cidade", Valores.Municipio);
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
        /// Função para compactar os arquivos
        /// </summary>
        /// <param name="Arquivos">Um ou mais arquivos que serão compactados</param>
        /// <param name="NomeArquivo">Nome do arquivo compactado</param>
        private void CompactarArquivos(List<string> Arquivos, string NomeArquivo)
        {

            using (ZipFile Zip = new ZipFile())
            {
                Zip.Password = ConfigurationManager.AppSettings["pwdZip"];
                Zip.AddFiles(Arquivos, false, "");
                Zip.Save(System.IO.Path.Combine(ConfigurationManager.AppSettings["DiretorioEmail"], NomeArquivo));
                Zip.Dispose();
            }
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
