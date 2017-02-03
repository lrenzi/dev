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
    public class DocumentoClienteTipoDal : BaseRepository
    {
        #region CRUD

        public Int64 Insert(DocumentoClienteTipo documentoClienteTipo)
        {
            Int64 recordId = new DapperSqlHelper().InsertWithReturnId(documentoClienteTipo);
            return recordId;
        }

        public DocumentoClienteTipo Update(DocumentoClienteTipo DocumentoClienteTipo)
        {
            bool update = new DapperSqlHelper().Update<DocumentoClienteTipo>(DocumentoClienteTipo);
            return DocumentoClienteTipo;
        }

        public bool Delete(int pDocCliTipoId)
        {
            return new DapperSqlHelper().Delete<DocumentoClienteTipo>(new DocumentoClienteTipo() { DocCliTipoId = pDocCliTipoId });
        }

        public IList<DocumentoClienteTipo> GetAll()
        {
            return new DapperSqlHelper().GetAll<DocumentoClienteTipo>();
        }

        public DocumentoClienteTipo GetDocumentoClienteTipo(int pDocCliSituId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliTipoId", pDocCliSituId, DbType.Int64, ParameterDirection.Input, null);
            var documentoClienteTipo = new DapperSqlHelper().QuerySP<DocumentoClienteTipo>("ConsultarDocumentoClienteTipo", p, null, null, false, 0);
            return (DocumentoClienteTipo)documentoClienteTipo.FirstOrDefault();
        }

        #endregion

        #region Customizados

        public IEnumerable<DocumentoClienteTipo> GetAllByIdCliente(int clienteId)
        {
            var p = new DynamicParameters();
            p.Add("@pClienteId", clienteId, DbType.Int32, null);

            var documentoClienteTipo = new DapperSqlHelper().QuerySP<DocumentoClienteTipo>("ConsultarDocumentoClienteTipo", p, null, null, false, 0);
            return documentoClienteTipo;
        }

        #endregion
    }
}
