using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.Entity
{
    public class Cliente
    {
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

        public EnumCliente ClienteNomeEnum
        {
            get
            {
                return (EnumCliente)ClienteId;
            }
        }
    }
}
