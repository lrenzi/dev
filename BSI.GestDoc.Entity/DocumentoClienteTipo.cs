using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.Entity
{
    public class DocumentoClienteTipo: EntityBase
    {
        public int DocCliTipoId { get; set; }
        public int ClienteId { get; set; }
        public string DocCliTipoNome { get; set; }
        public string DocCliTipoDescricao { get; set; }
        public int DocCliTipoOrdemApresent { get; set; }

        //Auxiliar
        public string Reenvio { get; set; }
    }
}
