using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class DocumentoClienteSituacaoDal
    {
        #region CRUD

        public Int64 Insert(DocumentoClienteSituacao DocumentoClienteSituacao)
        {
            Int64 recordId = SqlHelper.InsertWithReturnId(DocumentoClienteSituacao);
            return recordId;
        }

        public DocumentoClienteSituacao Update(DocumentoClienteSituacao DocumentoClienteSituacao)
        {
            bool update = SqlHelper.Update<DocumentoClienteSituacao>(DocumentoClienteSituacao);
            return DocumentoClienteSituacao;
        }


        public bool Delete(long pDocCliSituId)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<DocumentoClienteSituacao>(f => f.DocCliSituId, Operator.Eq, pDocCliSituId, true));

            return SqlHelper.Delete<DocumentoClienteSituacao>(pg);
        }

        public IList<DocumentoClienteSituacao> GetAll()
        {
            return SqlHelper.GetAll<DocumentoClienteSituacao>();
        }

        public DocumentoClienteSituacao GetDocumentoClienteSituacao(int pDocCliSituId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliSituId", pDocCliSituId, DbType.Int64, ParameterDirection.Input, null);
            var documentoCliente = SqlHelper.QuerySP<DocumentoClienteSituacao>("ConsultarDocumentoClienteSituacao", p, null, null, false, 0);
            return (DocumentoClienteSituacao)documentoCliente.FirstOrDefault();
        }

        #endregion

        #region Customizados

        public IEnumerable<DocumentoClienteSituacao> GetAllDocumentoClienteSituacaoByDocCliTipoId(int pDocCliTipoId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliTipoId", pDocCliTipoId, DbType.Int32, ParameterDirection.Input);

            var DocumentoClienteSituacao = SqlHelper.QuerySP<DocumentoClienteSituacao>("ConsultarDocumentoClienteSituacao", p, null, null, false, 0);
            return DocumentoClienteSituacao;
        }

        /// <summary>
        /// Lista Situações por Tipo de Documento
        /// </summary>
        /// <param name="docCliTipoId"></param>
        /// <returns></returns>
        public IEnumerable<DocumentoClienteSituacao> ListarSituacaoDocumentoCliente(int docCliTipoId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pDocCliTipoId", docCliTipoId, DbType.Int16, null);

            var listaSituacaoDocumentoCliente = SqlHelper.QuerySP<DocumentoClienteSituacao>("ConsultarDocumentoClienteSituacao", parameters);
            return listaSituacaoDocumentoCliente;
        }

        /// <summary>
        /// Recupera lista de situações pelo tipo do documento
        /// </summary>
        /// <param name="codTipoDocumento"></param>
        /// <returns></returns>
        public IEnumerable<DocumentoClienteSituacao> ListarSituacaoDocumento(string codTipoDocumento)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@pDocCliTipoId", codTipoDocumento, DbType.Int16, null);

            var listaSituacaoDocumento = SqlHelper.QuerySP<DocumentoClienteSituacao>("ConsultarDocumentoClienteSituacao", parameters);
            return listaSituacaoDocumento;
        }

        #endregion
    }
}
