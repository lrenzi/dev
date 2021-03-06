﻿using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using BSI.GestDoc.Repository.Base;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository
{
    public class EnviarArquivoDal: BaseRepository
    {
        public IEnumerable<DocumentoCliente> ConsultarDocumentoClientePorDocCliDadosValorDocCliTipoId(String pDocCliDadosValor, int pDocCliTipoId)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliDadosValor", pDocCliDadosValor, DbType.String, ParameterDirection.Input, 100);
            p.Add("@pDocCliTipoId", pDocCliTipoId, DbType.Int32, ParameterDirection.Input);

            var DocumentoCliente = new DapperSqlHelper().QuerySP<DocumentoCliente>("ConsultarDocumentoClientePorDocCliDadosValorDocCliTipoId", p, null, null, false, 0);
            return DocumentoCliente;
        }

        public IEnumerable<DocumentoCliente> ConsultarNumeroPropostaPorUsuario(String pDocCliDadosValor)
        {
            var p = new DynamicParameters();
            p.Add("@pDocCliDadosValor", pDocCliDadosValor, DbType.String, ParameterDirection.Input, 100);

            var DocumentoCliente = new DapperSqlHelper().QuerySP<DocumentoCliente>("ConsultarDocumentoClientePorDocCliDadosValor", p, null, null, false, 0);
            return DocumentoCliente;
        }

        public DocumentoClienteDados InserirDocumentoClienteDados(DocumentoClienteDados documentoClienteDados_)
        {
            Int64 recordId = new DapperSqlHelper().InsertWithReturnId(documentoClienteDados_);
            documentoClienteDados_.DocCliDadosId = recordId;
            return documentoClienteDados_;
        }
    }
}
