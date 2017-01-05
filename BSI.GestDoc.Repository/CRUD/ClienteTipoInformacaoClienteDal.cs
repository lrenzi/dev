using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class ClienteTipoInformacaoClienteDal
    {
        #region CRUD

        public Int64 Insert(ClienteTipoInformacaoCliente ClienteTipoInformacaoCliente)
        {
            Int64 recordId = SqlHelper.InsertWithReturnId(ClienteTipoInformacaoCliente);
            return recordId;
        }

        public ClienteTipoInformacaoCliente Update(ClienteTipoInformacaoCliente ClienteTipoInformacaoCliente)
        {
            bool update = SqlHelper.Update<ClienteTipoInformacaoCliente>(ClienteTipoInformacaoCliente);
            return ClienteTipoInformacaoCliente;
        }

        public bool Delete(long pCliTipoInfoCliId)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<ClienteTipoInformacaoCliente>(f => f.CliTipoInfoCliId, Operator.Eq, pCliTipoInfoCliId, true));

            return SqlHelper.Delete<ClienteTipoInformacaoCliente>(pg);
        }

        public IList<ClienteTipoInformacaoCliente> GetAll()
        {
            return SqlHelper.GetAll<ClienteTipoInformacaoCliente>();
        }

        public ClienteTipoInformacaoCliente GetClienteTipoInformacaoCliente(int pCliTipoInfoCliId)
        {
            var p = new DynamicParameters();
            p.Add("@pCliTipoInfoCliId", pCliTipoInfoCliId, DbType.String, null);

            var Cliente = SqlHelper.QuerySP<ClienteTipoInformacaoCliente>("ConsultarClienteTipoInformacaoCliente", p, null, null, false, 0);
            return (ClienteTipoInformacaoCliente)Cliente.FirstOrDefault();
        }

        #endregion

        #region Customizados

        public IEnumerable<ClienteTipoInformacaoCliente> GetAllByIdCliente(Int64 pClienteId)
        {
            var p = new DynamicParameters();
            p.Add("@pClienteId", pClienteId, DbType.String, null);

            var ClienteTipoInformacaoCliente = SqlHelper.QuerySP<ClienteTipoInformacaoCliente>("ConsultarClienteTipoInformacaoCliente", p, null, null, false, 0);
            return ClienteTipoInformacaoCliente;
        }

        #endregion
    }
}
