using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.DAL
{
    public class DocumentoClienteTipoDal
    {
        public int InsertDocumentoClienteTipo(DocumentoClienteTipo documentoClienteTipo)
        {
            int recordId = SqlHelper.InsertWithReturnId(documentoClienteTipo);
            return recordId;
        }

        public IList<DocumentoClienteTipo> GetAllDocumentoClienteTipo()
        {
            return SqlHelper.GetAll<DocumentoClienteTipo>();
        }

        public IEnumerable<DocumentoClienteTipo> GetAllDocumentoClienteTipo(string spName, string connectionString)
        {
            var user = SqlHelper.QuerySP<DocumentoClienteTipo>(spName, null, null, null, false, 0);
            return user;
        }

        public IEnumerable<DocumentoClienteTipo> GetAllDocumentoClienteTipoByIdCliente(int clienteId)
        {
            var p = new DynamicParameters();
            p.Add("@ClienteId", clienteId, DbType.String, null);

            var documentoClienteTipo = SqlHelper.QuerySP<DocumentoClienteTipo>("ConsultarDocumentoClienteTipoPorCliente", p, null, null, false, 0);
            return documentoClienteTipo;
        }

        public DocumentoClienteTipo UpdateDocumentoClienteTipo(DocumentoClienteTipo documentoClienteTipo)
        {
            throw new NotImplementedException();
        }

        public DocumentoClienteTipo GetByDocumentoClienteTipoId(string spName, DynamicParameters documentoClienteTipoId, string connectionString)
        {
            var user = SqlHelper.QuerySP<DocumentoClienteTipo>(spName, documentoClienteTipoId, null, null, false, 0);
            return (DocumentoClienteTipo)user.FirstOrDefault();
        }

        public DocumentoClienteTipo GetDocumentoClienteTipo(int docCliTipoId)
        {
            throw new NotImplementedException();
        }
    }
}
