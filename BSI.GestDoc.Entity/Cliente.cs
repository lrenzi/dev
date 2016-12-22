using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("Cliente")]
    public class Cliente
    {
        [DapperAttribute.Key]
        public Int64 ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClientePastaDocumentos { get; set; }
        public string ClienteImagemLogoDesktop { get; set; }
        public string ClienteImagemLogoMobile { get; set; }
        public string ClienteCorPadrao { get; set; }

        public enum EnumCliente
        {
            Bradesco = 1
        }

        [DapperAttribute.Computed]
        [DapperAttribute.Write(false)]
        public EnumCliente ClienteNomeEnum
        {
            get
            {
                return (EnumCliente)ClienteId;
            }
        }
    }
}
