using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class DocumentoClienteDadosDocDal
    {
        public DocumentoClienteDadosDoc InsertDocumentoClienteDadosDoc(DocumentoClienteDadosDoc DocumentoClienteDadosDoc)
        {
            int recordId = SqlHelper.InsertWithReturnId(DocumentoClienteDadosDoc);
            DocumentoClienteDadosDoc.DocCliDadosDocId = recordId;
            return DocumentoClienteDadosDoc;
        }

        public IList<DocumentoClienteDadosDoc> GetAllDocumentoClienteDadosDoc()
        {
            return SqlHelper.GetAll<DocumentoClienteDadosDoc>();
        }

        public IEnumerable<DocumentoClienteDadosDoc> GetAllDocumentoClienteDadosDoc(string spName, string connectionString)
        {
            var user = SqlHelper.QuerySP<DocumentoClienteDadosDoc>(spName, null, null, null, false, 0);
            return user;
        }

        public IEnumerable<DocumentoClienteDadosDoc> GetAllDocumentoClienteDadosDocByDocClienteId(long docClienteId)
        {
            var p = new DynamicParameters();
            p.Add("@DocClienteId", docClienteId, DbType.Int64, null);

            var DocumentoClienteDadosDoc = SqlHelper.QuerySP<DocumentoClienteDadosDoc>("ConsultarDocumentoClienteDadosDoc", p, null, null, false, 0);
            return DocumentoClienteDadosDoc;
        }

        public IEnumerable<DocumentoClienteDadosDoc> GetAllDocumentoClienteDadosDocByDocCliDadosId(long docCliDadosId)
        {
            var p = new DynamicParameters();
            p.Add("@DocCliDadosId", docCliDadosId, DbType.Int64, null);

            var DocumentoClienteDadosDoc = SqlHelper.QuerySP<DocumentoClienteDadosDoc>("ConsultarDocumentoClienteDadosDoc", p, null, null, false, 0);
            return DocumentoClienteDadosDoc;
        }

        public DocumentoClienteDadosDoc UpdateDocumentoClienteDadosDoc(DocumentoClienteDadosDoc DocumentoClienteDadosDoc)
        {
            throw new NotImplementedException();
        }

        public DocumentoClienteDadosDoc GetByDocumentoClienteDadosDocId(string spName, DynamicParameters DocumentoClienteDadosDocId, string connectionString)
        {
            var user = SqlHelper.QuerySP<DocumentoClienteDadosDoc>(spName, DocumentoClienteDadosDocId, null, null, false, 0);
            return (DocumentoClienteDadosDoc)user.FirstOrDefault();
        }

        public DocumentoClienteDadosDoc GetDocumentoClienteDadosDoc(int docCliTipoId)
        {
            throw new NotImplementedException();
        }
    }
}
