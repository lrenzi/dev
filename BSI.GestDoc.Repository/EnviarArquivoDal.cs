using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository
{
    public class EnviarArquivoDal
    {
        

        public List<DocumentoClienteDados> ConsultarNumeroPropostaPorUsuario(String pDocCliDadosValor, long pUsuarioId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliDadosValor", pDocCliDadosValor, DbType.String, ParameterDirection.Input, 100);
            p.Add("@pUsuarioId", pUsuarioId, DbType.Int64, ParameterDirection.Input);

            var DocumentoClienteDados = SqlHelper.QuerySP<DocumentoClienteDados>("ConsultarNumeroPropostaPorUsuario", p, null, null, false, 0);
            return DocumentoClienteDados.ToList();
        }


        public DocumentoClienteDados InserirDocumentoClienteDados(DocumentoClienteDados documentoClienteDados_)
        {
            int recordId = SqlHelper.InsertWithReturnId(documentoClienteDados_);
            documentoClienteDados_.DocCliDadosId = recordId;
            return documentoClienteDados_;
        }

        public DocumentoCliente InserirDocumentoCliente(DocumentoCliente documentoCliente_)
        {
            int recordId = SqlHelper.InsertWithReturnId(documentoCliente_);
            documentoCliente_.DocClienteId = recordId;
            return documentoCliente_;
        }

    }
}
