using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.Base;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class DocumentoClienteDal : BaseRepository
    {
        #region CRUD
        public DocumentoCliente Insert(DocumentoCliente DocumentoCliente)
        {
            Int64 recordId = new DapperSqlHelper().InsertWithReturnId(DocumentoCliente);
            DocumentoCliente.ClienteId = recordId;
            return DocumentoCliente;
        }

        public DocumentoCliente Update(DocumentoCliente DocumentoCliente)
        {
            bool update = new DapperSqlHelper().Update<DocumentoCliente>(DocumentoCliente);
            return DocumentoCliente;
        }

        public bool Delete(DocumentoCliente DocumentoCliente)
        {
            bool delete = new DapperSqlHelper().Delete<DocumentoCliente>(DocumentoCliente);
            return delete;
        }

        public IList<DocumentoCliente> GetAll()
        {
            return new DapperSqlHelper().GetAll<DocumentoCliente>();
        }

        public DocumentoCliente GetDocumentoCliente(Int64 pDocClienteId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocClienteId", pDocClienteId, DbType.Int64, ParameterDirection.Input, null);
            var documentoCliente = new DapperSqlHelper().QuerySP<DocumentoCliente>("ConsultarDocumentoCliente", p, null, null, false, 0);
            return (DocumentoCliente)documentoCliente.FirstOrDefault();
        }

        #endregion

        #region Customizados

        public IEnumerable<DocumentoCliente> GetAllByUsuarioIdDocCliTipoIdDocCliSituId(long idUsuario, int docCliTipoId, int docCliSituId)
        {
            var p = new DynamicParameters();
            p.Add("@UsuarioId", idUsuario, DbType.Int64, null);
            p.Add("@DocCliTipoId", docCliTipoId, DbType.Int64, null);
            p.Add("@DocCliSituId", docCliSituId, DbType.Int64, null);

            var DocumentoCliente = new DapperSqlHelper().QuerySP<DocumentoCliente>("ConsultarDocumentoCliente", p, null, null, false, 0);
            return DocumentoCliente;
        }

        
        public IEnumerable<DocumentoCliente> GetDocumentosByClienteUsuario(Int64 pDocClienteId, Int64 pUsuarioId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocClienteId", pDocClienteId, DbType.Int64, ParameterDirection.Input, null);
            var DocumentosRetorno = new DapperSqlHelper().QuerySP<DocumentoCliente>("ConsultarDocumentoCliente", p, null, null, false, 0);
            return DocumentosRetorno;
        }


        #endregion
    }
}
