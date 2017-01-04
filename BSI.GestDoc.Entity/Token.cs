using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("Token")]
    public class Token
    {
        [DapperAttribute.Key]
        public Int64 TokenId { get; set; }
        public string Id { get; set; }
        public string IdentityName { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public Int64 UsuarioId { get; set; }

        public string ProtectedTicket { get; set; }
    }
}
