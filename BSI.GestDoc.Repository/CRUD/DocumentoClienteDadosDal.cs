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
    public class DocumentoClienteDadosDal
    {
        #region CRUD

        public Int64 Insert(DocumentoClienteDados DocumentoClienteDados)
        {
            Int64 recordId = SqlHelper.InsertWithReturnId(DocumentoClienteDados);
            return recordId;
        }

        public DocumentoClienteDados Update(DocumentoClienteDados DocumentoClienteDados)
        {
            bool update = SqlHelper.Update<DocumentoClienteDados>(DocumentoClienteDados);
            return DocumentoClienteDados;
        }

        public bool Delete(long pDocCliDadosId)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<DocumentoClienteDados>(f => f.DocCliDadosId, Operator.Eq, pDocCliDadosId, true));

            return SqlHelper.Delete<DocumentoClienteDados>(pg);
        }

        public IList<DocumentoClienteDados> GetAll()
        {
            return SqlHelper.GetAll<DocumentoClienteDados>();
        }


        public DocumentoClienteDados GetDocumentoClienteDados(long pDocCliDadosId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliDadosId", pDocCliDadosId, DbType.Int64, null);

            var Cliente = SqlHelper.QuerySP<DocumentoClienteDados>("ConsultarDocumentoClienteDados", p, null, null, false, 0);
            return (DocumentoClienteDados)Cliente.FirstOrDefault();
        }

        #endregion

        #region Customizados

        public IEnumerable<DocumentoClienteDados> GetAllByDocCliDadosValor(string pDocCliDadosValor)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliDadosValor", pDocCliDadosValor, DbType.String, ParameterDirection.Input, 100);

            var DocumentoClienteDados = SqlHelper.QuerySP<DocumentoClienteDados>("ConsultarDocumentoClienteDados", p, null, null, false, 0);
            return DocumentoClienteDados;
        }

        public IEnumerable<DocumentoClienteDados> GetAllByUsuarioIdClienteId(Int16 pClienteId, Int16 pUsuarioId )
        {
            var p = new DynamicParameters();
            p.Add("@pClienteId", pClienteId, DbType.Int16, null);
            p.Add("@pUsuarioId", pUsuarioId, DbType.Int16, null);

            var DocumentoClienteDados = SqlHelper.QuerySP<DocumentoClienteDados>("ConsultarDocumentoClienteDadosPorUsuario", p, null, null, false, 0);
            return DocumentoClienteDados;
        }

        #endregion
    }
}
