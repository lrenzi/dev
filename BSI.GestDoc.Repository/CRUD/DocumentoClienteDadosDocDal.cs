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
    public class DocumentoClienteDadosDocDal: BaseRepository
    {
        #region CRUD

        public DocumentoClienteDadosDoc Insert(DocumentoClienteDadosDoc DocumentoClienteDadosDoc)
        {
            Int64 recordId = new DapperSqlHelper().InsertWithReturnId(DocumentoClienteDadosDoc);
            DocumentoClienteDadosDoc.DocCliDadosDocId = recordId;
            return DocumentoClienteDadosDoc;
        }


        public DocumentoClienteDadosDoc Update(DocumentoClienteDadosDoc DocumentoClienteDadosDoc)
        {
            bool update = new DapperSqlHelper().Update<DocumentoClienteDadosDoc>(DocumentoClienteDadosDoc);
            return DocumentoClienteDadosDoc;
        }

        public bool Delete(DocumentoClienteDadosDoc DocumentoClienteDadosDoc)
        {
            bool delete = new DapperSqlHelper().Delete<DocumentoClienteDadosDoc>(DocumentoClienteDadosDoc);
            return delete;
        }

        public IList<DocumentoClienteDadosDoc> GetAll()
        {
            return new DapperSqlHelper().GetAll<DocumentoClienteDadosDoc>();
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
