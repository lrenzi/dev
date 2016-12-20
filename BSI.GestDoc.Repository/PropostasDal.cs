using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BSI.GestDoc.Repository.DAL
{
    public class PropostasDal
    {
        public IEnumerable<DocumentoClienteDados> ListarPropostas(string usuarioId, string clienteId)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@pClienteId", clienteId, DbType.String, null);
            parameters.Add("@pUsuarioId", usuarioId, DbType.String, null);

            var dadosDocumentoClienteRetorno = this.QuerySPCustom("ConsultarDocumentoClienteDadosPorUsuario", parameters);


            return dadosDocumentoClienteRetorno;
        }

        private  IEnumerable<DocumentoClienteDados> QuerySPCustom(String storedProcedure, DynamicParameters parameters)
        {
            SqlConnection connection = SqlHelper.getConnection();
            Usuario usuarioLogado = new Usuario();
            IEnumerable<DocumentoClienteDados> documentoClienteRetorno = null;

            using (SqlMapper.GridReader reader = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure))
            {
                //recupera dados do cliente e informações referenciadas
                //documentoClienteRetorno = reader.Read<DocumentoClienteDados, DocumentoClienteDadosDoc, DocumentoCliente, DocumentoClienteDados>((documentoDados, documentoClienteDadosDoc, documentoCliente) =>
                //{
                //    documentoDados.DocumentoClienteDadosDoc = documentoClienteDadosDoc;
                //    documentoDados.DocumentoClienteDadosDoc.DocumentoCliente = documentoCliente;
                //    return documentoDados;
                //}, splitOn: "DocCliDadosId, ClienteId, TipoInfoCliId");


                documentoClienteRetorno = reader.Read<DocumentoClienteDados>();

            }

            return documentoClienteRetorno;
        }
    }
}
