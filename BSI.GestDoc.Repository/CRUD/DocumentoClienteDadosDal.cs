using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class DocumentoClienteDadosDal
    {
        public int InsertDocumentoClienteDados(DocumentoClienteDados DocumentoClienteDados)
        {
            int recordId = SqlHelper.InsertWithReturnId(DocumentoClienteDados);
            return recordId;
        }

        public IList<DocumentoClienteDados> GetAllDocumentoClienteDados()
        {
            return SqlHelper.GetAll<DocumentoClienteDados>();
        }

        public IEnumerable<DocumentoClienteDados> GetAllDocumentoClienteDados(string spName, string connectionString)
        {
            var user = SqlHelper.QuerySP<DocumentoClienteDados>(spName, null, null, null, false, 0);
            return user;
        }

        public IEnumerable<DocumentoClienteDados> GetAllDocumentoClienteDadosByDocCliDadosId(long docCliDadosId)
        {
            var p = new DynamicParameters();
            p.Add("@DocCliDadosId", docCliDadosId, DbType.Int64, null);

            var DocumentoClienteDados = SqlHelper.QuerySP<DocumentoClienteDados>("ConsultarDocumentoClienteDados", p, null, null, false, 0);
            return DocumentoClienteDados;
        }

        public IEnumerable<DocumentoClienteDados> GetAllDocumentoClienteDadosByDocCliDadosValor(string pDocCliDadosValor)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliDadosValor", pDocCliDadosValor, DbType.Int64, null);

            var DocumentoClienteDados = SqlHelper.QuerySP<DocumentoClienteDados>("ConsultarDocumentoClienteDados", p, null, null, false, 0);
            return DocumentoClienteDados;
        }

        public DocumentoClienteDados UpdateDocumentoClienteDados(DocumentoClienteDados DocumentoClienteDados)
        {
            throw new NotImplementedException();
        }

        public DocumentoClienteDados GetByDocumentoClienteDadosId(string spName, DynamicParameters DocumentoClienteDadosId, string connectionString)
        {
            var user = SqlHelper.QuerySP<DocumentoClienteDados>(spName, DocumentoClienteDadosId, null, null, false, 0);
            return (DocumentoClienteDados)user.FirstOrDefault();
        }

        public DocumentoClienteDados GetDocumentoClienteDados(int docCliTipoId)
        {
            throw new NotImplementedException();
        }
    }
}
