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
        /// <summary>
        /// Lista de Documentos dados por usuario
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="clienteId"></param>
        /// <param name="numeroProposta"></param>
        /// <returns></returns>
        public IEnumerable<DocumentoClienteDados> ListarPropostas(string usuarioId, string clienteId, string numeroProposta)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@pClienteId", clienteId, DbType.Int16, null);
            parameters.Add("@pUsuarioId", usuarioId, DbType.Int16, null);
            parameters.Add("@pDocCliDadosId", numeroProposta, DbType.Int16, null);

            var dadosDocumentoClienteRetorno = this.QuerySPCustom("ConsultarDocumentoClienteDadosPorUsuario", parameters);


            return dadosDocumentoClienteRetorno;
        }

        /// <summary>
        /// Consulta documento dados
        /// </summary>
        /// <param name="documentoCliente"></param>
        /// <returns></returns>
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
                    documentoCliente.DocumentoClienteSituacao = documentoClienteSituacao;
                    documentoCliente.DocumentoClienteTipo = documentoClienteTipo;                    
                    return documentoCliente;
                }, splitOn: "DocClienteId, DocCliSituId, DocCliTipoId");
            }

            return infoDocumentoClienteRetorno;
        }
    }
}
