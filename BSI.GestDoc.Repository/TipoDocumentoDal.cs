using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BSI.GestDoc.Repository.DAL
{
    public class TipoDocumentoDal
    {
        
        public IEnumerable<DocumentoClienteTipo> ListarTipoDocumento(string clienteId)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@ClienteId", clienteId, DbType.Int16, null);
            
            var listaTipos = SqlHelper.QuerySP<DocumentoClienteTipo>("ConsultarDocumentoClienteTipo", parameters);


            return listaTipos;
        }
    }
}
