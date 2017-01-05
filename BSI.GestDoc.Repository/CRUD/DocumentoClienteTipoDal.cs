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
    public class DocumentoClienteTipoDal
    {
        #region CRUD

        public Int64 Insert(DocumentoClienteTipo documentoClienteTipo)
        {
            Int64 recordId = SqlHelper.InsertWithReturnId(documentoClienteTipo);
            return recordId;
        }

        public DocumentoClienteTipo Update(DocumentoClienteTipo DocumentoClienteTipo)
        {
            bool update = SqlHelper.Update<DocumentoClienteTipo>(DocumentoClienteTipo);
            return DocumentoClienteTipo;
        }

        public bool Delete(long pDocCliTipoId)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<DocumentoClienteTipo>(f => f.DocCliTipoId, Operator.Eq, pDocCliTipoId, true));

            return SqlHelper.Delete<DocumentoClienteTipo>(pg);
        }

        public IList<DocumentoClienteTipo> GetAll()
        {
            return SqlHelper.GetAll<DocumentoClienteTipo>();
        }

        public DocumentoClienteTipo GetDocumentoClienteTipo(int pDocCliSituId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliTipoId", pDocCliSituId, DbType.Int64, ParameterDirection.Input, null);
            var documentoClienteTipo = SqlHelper.QuerySP<DocumentoClienteTipo>("ConsultarDocumentoClienteTipo", p, null, null, false, 0);
            return (DocumentoClienteTipo)documentoClienteTipo.FirstOrDefault();
        }

        #endregion

        #region Customizados

        public IEnumerable<DocumentoClienteTipo> GetAllByIdCliente(int clienteId)
        {
            var p = new DynamicParameters();
            p.Add("@pClienteId", clienteId, DbType.Int32, null);

            var documentoClienteTipo = SqlHelper.QuerySP<DocumentoClienteTipo>("ConsultarDocumentoClienteTipo", p, null, null, false, 0);
            return documentoClienteTipo;
        }

        ///// <summary>
        ///// Recupera lista de Tipo de documentos pelo clienteID logado
        ///// </summary>
        ///// <param name="clienteId"></param>
        ///// <returns></returns>
        //public IEnumerable<DocumentoClienteTipo> ListarTipoDocumento(string clienteId)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@pClienteId", clienteId, DbType.Int16, null);

        //    var listaTipos = SqlHelper.QuerySP<DocumentoClienteTipo>("ConsultarDocumentoClienteTipo", parameters);


        //    return listaTipos;
        //}

        #endregion
    }
}
