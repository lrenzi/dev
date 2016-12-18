using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class DocumentoClienteSituacaoDal
    {
        public int InsertDocumentoClienteSituacao(DocumentoClienteSituacao DocumentoClienteSituacao)
        {
            int recordId = SqlHelper.InsertWithReturnId(DocumentoClienteSituacao);
            return recordId;
        }

        public IList<DocumentoClienteSituacao> GetAllDocumentoClienteSituacao()
        {
            return SqlHelper.GetAll<DocumentoClienteSituacao>();
        }

        public IEnumerable<DocumentoClienteSituacao> GetAllDocumentoClienteSituacao(string spName, string connectionString)
        {
            var user = SqlHelper.QuerySP<DocumentoClienteSituacao>(spName, null, null, null, false, 0);
            return user;
        }

        public IEnumerable<DocumentoClienteSituacao> GetAllDocumentoClienteSituacaoByDocCliTipoId(int docCliTipoId)
        {
            var p = new DynamicParameters();
            p.Add("@DocCliTipoId", docCliTipoId, DbType.String, null);

            var DocumentoClienteSituacao = SqlHelper.QuerySP<DocumentoClienteSituacao>("ConsultarDocumentoClienteSituacao", p, null, null, false, 0);
            return DocumentoClienteSituacao;
        }

        public DocumentoClienteSituacao UpdateDocumentoClienteSituacao(DocumentoClienteSituacao DocumentoClienteSituacao)
        {
            throw new NotImplementedException();
        }

        public DocumentoClienteSituacao GetByDocumentoClienteSituacaoId(string spName, DynamicParameters DocumentoClienteSituacaoId, string connectionString)
        {
            var user = SqlHelper.QuerySP<DocumentoClienteSituacao>(spName, DocumentoClienteSituacaoId, null, null, false, 0);
            return (DocumentoClienteSituacao)user.FirstOrDefault();
        }

        public DocumentoClienteSituacao GetDocumentoClienteSituacao(int docCliTipoId)
        {
            throw new NotImplementedException();
        }
    }
}
