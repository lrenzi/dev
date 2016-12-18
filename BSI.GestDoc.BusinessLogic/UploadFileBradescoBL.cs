using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository;
using BSI.GestDoc.Repository.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.GestDoc.BusinessLogic
{
    public class UploadFileBradescoBL : UploadFileBL
    {
        public UploadFileBradescoBL() : base()
        {
        }

        public override DocumentoCliente EnviarDocumentoCliente(DocumentoCliente documentoCliente_)
        {
            UtilFile.UtilFileBradesco utilFileBradesco = new UtilFile.UtilFileBradesco();

            //Validações **

            #region 1 - Verifica tipo/ versão pdf
            if (BSI.GestDoc.Util.UtilFile.GetMIMEType(documentoCliente_.DocClienteNomeArquivoOriginal).ToLower() != "application/pdf")
            {
                throw new BusinessException.BusinessException("Erro - Documento deve ser do tipo PDF");
            }
            #endregion

            #region 2 - Verifica número proposta no PDF
            DocumentoClienteDados _documentoClienteDados = null;
            try
            {
                _documentoClienteDados = utilFileBradesco.LerPdf(documentoCliente_.DocClienteNomeArquivoSalvo);
                _documentoClienteDados.ClienteId = documentoCliente_.ClienteId;
                _documentoClienteDados.TipoInfoCliId = (new ClienteTipoInformacaoClienteDal().GetAllClienteTipoInformacaoClienteByIdCliente(_documentoClienteDados.ClienteId).First()).TipoInfoCliId;
            }
            catch (Exception ex)
            {
                throw new BusinessException.BusinessException("Erro ao recuperar o número do contrato. Erro [" + ex.Message + "]");
            }
            #endregion

            #region 3 - Consultar numero proposta por usuário na base
            //Não pode inserir um numero de proposta na base sem antes verificar se o numero da proposta já existe na base e se a proposta foi cadastrada pelo mesmo usuário que está enviando o arquivo.

            if (new EnviarArquivoDal().ConsultarNumeroPropostaPorUsuario(_documentoClienteDados.DocCliDadosValor.Trim(), documentoCliente_.UsuarioId).Count > 0)
            {
                throw new BusinessException.BusinessException("Erro: Arquivo já existe para esta proposta.");
            }
            /*
            List<DocumentoClienteSituacao> _documentosClienteSituacao = new DocumentoClienteSituacaoDal().GetAllDocumentoClienteSituacaoByDocCliTipoId(documentoCliente_.DocCliTipoId).ToList();
            DocumentoClienteSituacao _documentoClienteSituacao = new DocumentoClienteSituacao() { DocCliSituId = _documentosClienteSituacao.Min(p => p.DocCliSituId) };

            //Pesquisa o documento do cliente
            DocumentoCliente _documentoCliente = new DocumentoCliente();
            _documentoCliente.DocCliTipoId = _documentoClienteSituacao.DocCliTipoId;
            _documentoCliente.DocCliSituId = _documentoClienteSituacao.DocCliSituId;
            _documentoCliente.UsuarioId = documentoCliente_.UsuarioId;

            List<DocumentoCliente> _documentosCliente =
                new DocumentoClienteDal().GetAllDocumentoClienteByUsuarioIdDocCliTipoIdDocCliSituId(
                    _documentoCliente.UsuarioId,
                    _documentoCliente.DocCliTipoId,
                    _documentoCliente.DocCliSituId).ToList();

            foreach (DocumentoCliente itemDocumentoCliente in _documentosCliente)
            {
                List<DocumentoClienteDadosDoc> _documentosClienteDadosDoc =
                    new DocumentoClienteDadosDocDal().GetAllDocumentoClienteDadosDocByDocClienteId(itemDocumentoCliente.DocClienteId).ToList();
                foreach (DocumentoClienteDadosDoc itemDocumentoClienteDadosDoc in _documentosClienteDadosDoc)
                {
                    List<DocumentoClienteDados> _documentosClienteDados = new DocumentoClienteDadosDal().GetAllDocumentoClienteDadosByDocCliDadosId(itemDocumentoClienteDadosDoc.DocCliDadosId).ToList();
                    //Verifica se existe o numero da proposta
                    foreach (DocumentoClienteDados itemDocumentoClienteDados in _documentosClienteDados)
                    {
                        if (itemDocumentoClienteDados.DocCliDadosValor.Trim().ToUpper() == _documentoClienteDados.DocCliDadosValor.Trim().ToUpper())
                        {
                            throw new BusinessException.BusinessException("Erro: Arquivo já existe para esta proposta.");
                        }
                    }
                }
            }*/

            #endregion

            #region 5 - Consulta arquivos já existentes para o numero de proposta
            //Caso já exista um tipo e situação do arquivo na base igual ao que o usuário está tentando realizar o upload, irá perguntar ao usuário se ele deseja sobreescrever.
            #endregion

            #region 4 - Insere o numero proposta em base
            //Se o numero da proposta está OK e ainda não existe na base, irá realizar o insert na base, caso já exista irá utilizar o ID do numero de proposta que já existe na base
            List<DocumentoClienteDados> _documentosClienteDados = new DocumentoClienteDadosDal().GetAllDocumentoClienteDadosByDocCliDadosValor(_documentoClienteDados.DocCliDadosValor).ToList();
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

            documentoCliente_ = new EnviarArquivoDal().InserirDocumentoCliente(documentoCliente_);

            #endregion

            #region 7 - Faz insert na base p / relacionar id do documento e do num.de proposta

            DocumentoClienteDadosDoc _documentoClienteDadosDoc = new DocumentoClienteDadosDoc();
            _documentoClienteDadosDoc.DocClienteId = documentoCliente_.ClienteId;
            _documentoClienteDadosDoc.DocCliDadosId = documentoCliente_.DocClienteId;
            _documentoClienteDadosDoc = new DocumentoClienteDadosDocDal().InsertDocumentoClienteDadosDoc(_documentoClienteDadosDoc);

            #endregion

            return base.EnviarDocumentoCliente(documentoCliente_);
        }
    }
}
