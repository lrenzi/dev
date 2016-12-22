using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class ClienteTipoInformacaoClienteDal
    {
        public Int64 InsertClienteTipoInformacaoCliente(ClienteTipoInformacaoCliente ClienteTipoInformacaoCliente)
        {
            Int64 recordId = SqlHelper.InsertWithReturnId(ClienteTipoInformacaoCliente);
            return recordId;
        }

        public IList<ClienteTipoInformacaoCliente> GetAllClienteTipoInformacaoCliente()
        {
            return SqlHelper.GetAll<ClienteTipoInformacaoCliente>();
        }

        public IEnumerable<ClienteTipoInformacaoCliente> GetAllClienteTipoInformacaoCliente(string spName, string connectionString)
        {
            var user = SqlHelper.QuerySP<ClienteTipoInformacaoCliente>(spName, null, null, null, false, 0);
            return user;
        }

        public IEnumerable<ClienteTipoInformacaoCliente> GetAllClienteTipoInformacaoClienteByIdCliente(Int64 pClienteId)
        {
            var p = new DynamicParameters();
            p.Add("@pClienteId", pClienteId, DbType.String, null);

            var ClienteTipoInformacaoCliente = SqlHelper.QuerySP<ClienteTipoInformacaoCliente>("ConsultarClienteTipoInformacaoCliente", p, null, null, false, 0);
            return ClienteTipoInformacaoCliente;
        }

        public ClienteTipoInformacaoCliente UpdateClienteTipoInformacaoCliente(ClienteTipoInformacaoCliente ClienteTipoInformacaoCliente)
        {
            throw new NotImplementedException();
        }

        public ClienteTipoInformacaoCliente GetByClienteTipoInformacaoClienteId(string spName, DynamicParameters ClienteTipoInformacaoClienteId, string connectionString)
        {
            var user = SqlHelper.QuerySP<ClienteTipoInformacaoCliente>(spName, ClienteTipoInformacaoClienteId, null, null, false, 0);
            return (ClienteTipoInformacaoCliente)user.FirstOrDefault();
        }

        public ClienteTipoInformacaoCliente GetClienteTipoInformacaoCliente(int docCliTipoId)
        {
            throw new NotImplementedException();
        }
    }
}
