﻿using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BSI.GestDoc.Repository.DAL
{
    public class SituacaoDocumentoDal
    {
       /// <summary>
       /// Recupera lista de situações pelo tipo do documento
       /// </summary>
       /// <param name="codTipoDocumento"></param>
       /// <returns></returns>
        public IEnumerable<DocumentoClienteSituacao> ListarSituacaoDocumento(string codTipoDocumento)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@pDocCliTipoId", codTipoDocumento, DbType.Int16, null);
           

            var listaSituacaoDocumento = SqlHelper.QuerySP<DocumentoClienteSituacao>("ConsultarDocumentoClienteSituacao", parameters);


            return listaSituacaoDocumento;
        }

        
    }
}