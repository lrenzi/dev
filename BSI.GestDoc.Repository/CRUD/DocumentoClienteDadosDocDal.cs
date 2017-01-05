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
    public class DocumentoClienteDadosDocDal
    {
        #region CRUD

        public DocumentoClienteDadosDoc Insert(DocumentoClienteDadosDoc DocumentoClienteDadosDoc)
        {
            Int64 recordId = SqlHelper.InsertWithReturnId(DocumentoClienteDadosDoc);
            DocumentoClienteDadosDoc.DocCliDadosDocId = recordId;
            return DocumentoClienteDadosDoc;
        }


        public DocumentoClienteDadosDoc Update(DocumentoClienteDadosDoc DocumentoClienteDadosDoc)
        {
            bool update = SqlHelper.Update<DocumentoClienteDadosDoc>(DocumentoClienteDadosDoc);
            return DocumentoClienteDadosDoc;
        }

        public bool Delete(long pDocCliDadosDocId)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<DocumentoClienteDadosDoc>(f => f.DocCliDadosDocId, Operator.Eq, pDocCliDadosDocId, true));

            return SqlHelper.Delete<DocumentoClienteDadosDoc>(pg);
        }

        public IList<DocumentoClienteDadosDoc> GetAll()
        {
            return SqlHelper.GetAll<DocumentoClienteDadosDoc>();
        }

        public DocumentoClienteDadosDoc GetDocumentoClienteDadosDoc(int docCliTipoId)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Customizados



        #endregion

    }
}
