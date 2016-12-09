using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.Entity
{
    public class UsuarioPerfil
    {
        public int UsuPerfilId { get; set; }
        public int ClienteId { get; set; }
        public string UsuPerfilNome { get; set; }
        public string UsuPerfilDescricao { get; set; }
    }
}
