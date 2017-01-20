using BSI.GestDoc.BusinessLogic.Util;
using BSI.GestDoc.CustomException.BusinessException;
using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository;
using BSI.GestDoc.Repository.CRUD;
using BSI.GestDoc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
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

            #region 3 - Consultar numero proposta por usuário na base
            //Não pode inserir um numero de proposta na base sem antes verificar se o numero da proposta já existe na base e se a proposta foi cadastrada pelo mesmo usuário que está enviando o arquivo.

            //Recupera todos os documentos de clientes que possuem o mesmo número de proposta
            List<DocumentoCliente> _documentosClienteCadastrado = new EnviarArquivoDal().ConsultarNumeroPropostaPorUsuario(_documentoClienteDados.DocCliDadosValor.Trim()).ToList();
            if (_documentosClienteCadastrado.FindAll(p => p.UsuarioId != documentoCliente_.UsuarioId).Count > 0)
            {
                throw new BusinessException(0, EnumTipoMensagem.Alerta, "Proposta enviada por outro usuário.");
            }
            List<DocumentoClienteSituacao> _documentosClienteSituacao = new DocumentoClienteSituacaoDal().GetAllDocumentoClienteSituacaoByDocCliTipoId(documentoCliente_.DocCliTipoId).ToList();
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
    }
}
