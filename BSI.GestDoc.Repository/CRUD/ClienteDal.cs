﻿using BSI.Dapper.Helper;
using BSI.GestDoc.Entity;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BSI.GestDoc.Repository.CRUD
{
    public class ClienteDal
    {
        public Int64 Insert(Cliente Cliente)
        {
            Int64 recordId = SqlHelper.InsertWithReturnId(Cliente);
            return recordId;
        }

        public Cliente Update(Cliente Cliente)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long ClienteId)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<Cliente>(f => f.ClienteId, Operator.Eq, ClienteId, true));

            return SqlHelper.Delete<Token>(pg);
        }

        public IList<Cliente> GetAllCliente()
        {
            return SqlHelper.GetAll<Cliente>();
        }

        public Cliente GetCliente(Int64 pClienteId)
        {
            var p = new DynamicParameters();
            p.Add("@pClienteId", pClienteId, DbType.String, null);

            var Cliente = SqlHelper.QuerySP<Cliente>("ConsultarCliente", p, null, null, false, 0);
            return (Cliente)Cliente.FirstOrDefault();
        }
    }
}