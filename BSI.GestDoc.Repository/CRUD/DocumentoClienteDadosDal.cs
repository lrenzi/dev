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
    public class DocumentoClienteDadosDal: BaseRepository
    {
        #region CRUD

        public Int64 Insert(DocumentoClienteDados DocumentoClienteDados)
        {
            Int64 recordId = new DapperSqlHelper().InsertWithReturnId(DocumentoClienteDados);
            return recordId;
        }

        public DocumentoClienteDados Update(DocumentoClienteDados DocumentoClienteDados)
        {
            bool update = new DapperSqlHelper().Update<DocumentoClienteDados>(DocumentoClienteDados);
            return DocumentoClienteDados;
        }

        public bool Delete(DocumentoClienteDados DocumentoClienteDados)
        {
            bool delete = new DapperSqlHelper().Delete<DocumentoClienteDados>(DocumentoClienteDados);
            return delete;
        }

        public IList<DocumentoClienteDados> GetAll()
        {
            return new DapperSqlHelper().GetAll<DocumentoClienteDados>();
        }


        public DocumentoClienteDados GetDocumentoClienteDados(long pDocCliDadosId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliDadosId", pDocCliDadosId, DbType.Int64, null);

            var Cliente = new DapperSqlHelper().QuerySP<DocumentoClienteDados>("ConsultarDocumentoClienteDados", p, null, null, false, 0);
            return (DocumentoClienteDados)Cliente.FirstOrDefault();
        }

        #endregion

        #region Customizados

        public IEnumerable<DocumentoClienteDados> GetAllByDocCliDadosValor(string pDocCliDadosValor)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliDadosValor", pDocCliDadosValor, DbType.String, ParameterDirection.Input, 100);

            var DocumentoClienteDados = new DapperSqlHelper().QuerySP<DocumentoClienteDados>("ConsultarDocumentoClienteDados", p, null, null, false, 0);
            return DocumentoClienteDados;
        }

        public IEnumerable<DocumentoClienteDados> GetAllByUsuarioIdClienteId(Int16 pClienteId, Int16 pUsuarioId )
        {
            var p = new DynamicParameters();
            p.Add("@pClienteId", pClienteId, DbType.Int16, null);
            p.Add("@pUsuarioId", pUsuarioId, DbType.Int16, null);

            var DocumentoClienteDados = new DapperSqlHelper().QuerySP<DocumentoClienteDados>("ConsultarDocumentoClienteDadosPorUsuario", p, null, null, false, 0);
            return DocumentoClienteDados;
        }
        #endregion
    }
}
