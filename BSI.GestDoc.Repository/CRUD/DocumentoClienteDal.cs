using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class DocumentoClienteDal
    {
        public DocumentoCliente InsertDocumentoCliente(DocumentoCliente DocumentoCliente)
        {
            int recordId = SqlHelper.InsertWithReturnId(DocumentoCliente);
            DocumentoCliente.ClienteId = recordId;
            return DocumentoCliente;
        }

        public IList<DocumentoCliente> GetAllDocumentoCliente()
        {
            return SqlHelper.GetAll<DocumentoCliente>();
        }

        public IEnumerable<DocumentoCliente> GetAllDocumentoCliente(string spName, string connectionString)
        {
            var user = SqlHelper.QuerySP<DocumentoCliente>(spName, null, null, null, false, 0);
            return user;
        }

        public IEnumerable<DocumentoCliente> GetAllDocumentoClienteByUsuarioIdDocCliTipoIdDocCliSituId(long idUsuario, int docCliTipoId, int docCliSituId)
        {
            var p = new DynamicParameters();
            p.Add("@UsuarioId", idUsuario, DbType.Int64, null);
            p.Add("@DocCliTipoId", docCliTipoId, DbType.Int64, null);
            p.Add("@DocCliSituId", docCliSituId, DbType.Int64, null);

            var DocumentoCliente = SqlHelper.QuerySP<DocumentoCliente>("ConsultarDocumentoCliente", p, null, null, false, 0);
            return DocumentoCliente;
        }

        public DocumentoCliente UpdateDocumentoCliente(DocumentoCliente DocumentoCliente)
        {
            bool update = SqlHelper.Update<DocumentoCliente>(DocumentoCliente);
            return DocumentoCliente;
        }

        public DocumentoCliente GetByDocumentoClienteId(string spName, DynamicParameters DocumentoClienteId, string connectionString)
        {
            var user = SqlHelper.QuerySP<DocumentoCliente>(spName, DocumentoClienteId, null, null, false, 0);
            return (DocumentoCliente)user.FirstOrDefault();
        }

        public DocumentoCliente GetDocumentoCliente(int docCliTipoId)
        {
            throw new NotImplementedException();
        }
    }
}
