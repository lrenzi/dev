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
        

        public IEnumerable<DocumentoCliente> ConsultarNumeroPropostaPorUsuario(String pDocCliDadosValor)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliDadosValor", pDocCliDadosValor, DbType.String, ParameterDirection.Input, 100);

            var DocumentoCliente = SqlHelper.QuerySP<DocumentoCliente>("ConsultarDocumentoClientePorDocCliDadosValor", p, null, null, false, 0);
            return DocumentoCliente;
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
