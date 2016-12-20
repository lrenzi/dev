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

        public IEnumerable<DocumentoCliente> ConsultarInfoDocumentoCliente(DocumentoClienteDados documentoCliente)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@pClienteId", documentoCliente.ClienteId, DbType.Int16, null);
            parameters.Add("@pTipoInfoCliId", documentoCliente.TipoInfoCliId, DbType.Int16, null);
            parameters.Add("@pDocCliDadosValor", documentoCliente.DocCliDadosValor, DbType.String, null);

            var dadosInfoDocumentoCliente = this.QuerySPCustomInfoDocumentos("ConsultarDocumentoClientePorValorDado", parameters);

            return dadosInfoDocumentoCliente;
        }

        private  IEnumerable<DocumentoClienteDados> QuerySPCustom(String storedProcedure, DynamicParameters parameters)
        {
            SqlConnection connection = SqlHelper.getConnection();
            Usuario usuarioLogado = new Usuario();
            IEnumerable<DocumentoClienteDados> documentoClienteRetorno = null;

            using (SqlMapper.GridReader reader = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure))
            {
                documentoClienteRetorno = reader.Read<DocumentoClienteDados>();
            }

            return documentoClienteRetorno;
        }


        private IEnumerable<DocumentoCliente> QuerySPCustomInfoDocumentos(String storedProcedure, DynamicParameters parameters)
        {
            SqlConnection connection = SqlHelper.getConnection();
            Usuario usuarioLogado = new Usuario();
            IEnumerable<DocumentoCliente> infoDocumentoClienteRetorno = null;

            using (SqlMapper.GridReader reader = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure))
            {
                //recupera dados do cliente e informações referenciadas
                infoDocumentoClienteRetorno = reader.Read<DocumentoCliente, DocumentoClienteSituacao, DocumentoClienteTipo, DocumentoCliente>((documentoCliente, documentoClienteSituacao, documentoClienteTipo) =>
                {
                    documentoCliente.DocumentoClienteTipo = documentoClienteTipo;
                    documentoCliente.DocumentoClienteSituacao = documentoClienteSituacao;
                    return documentoCliente;
                }, splitOn: "DocCliTipoId, DocCliSituId, DocClienteId");
            }

            return infoDocumentoClienteRetorno;
        }
    }
}
