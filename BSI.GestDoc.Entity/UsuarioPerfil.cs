using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("UsuarioPerfil")]
    public class UsuarioPerfil
    {
        [DapperAttribute.Key]
        public int UsuPerfilId { get; set; }
        public int ClienteId { get; set; }
        public string UsuPerfilNome { get; set; }
        public string UsuPerfilDescricao { get; set; }
    }
}
