using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.Entity
{
    public class DocumentoClienteSituacao
    {
        public int DocCliSituId { get; set; }
        public string DocCliSituDescricao { get; set; }
        public int DocCliTipoId { get; set; }
        public int DocCliSituOrdemApresent { get; set; }
        
    }
}
