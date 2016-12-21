using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BSI.GestDoc.Repository.DAL
{
    public class DocumentoClienteSituacaoDal
    {
        /// <summary>
        /// Lista Situações por Tipo de Documento
        /// </summary>
        /// <param name="docCliTipoId"></param>
        /// <returns></returns>
        public IEnumerable<DocumentoClienteSituacao> ListarSituacaoDocumentoCliente(string docCliTipoId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pDocCliTipoId", docCliTipoId, DbType.Int16, null);
            
            var listaSituacaoDocumentoCliente = SqlHelper.QuerySP<DocumentoClienteSituacao>("ConsultarDocumentoClienteSituacao", parameters);

            return listaSituacaoDocumentoCliente;
        }

    }
}
