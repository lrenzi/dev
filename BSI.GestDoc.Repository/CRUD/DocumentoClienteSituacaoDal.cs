﻿using BSI.Dapper.Helper;
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
        public Int64 InsertDocumentoClienteSituacao(DocumentoClienteSituacao DocumentoClienteSituacao)
        {
            Int64 recordId = SqlHelper.InsertWithReturnId(DocumentoClienteSituacao);
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

        public IEnumerable<DocumentoClienteSituacao> GetAllDocumentoClienteSituacaoByDocCliTipoId(int pDocCliTipoId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliTipoId", pDocCliTipoId, DbType.Int32, ParameterDirection.Input);

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
