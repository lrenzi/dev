using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BSI.GestDoc.WebAPI.Models
{
    public class UsuarioModel
    {
        [Required(ErrorMessage = "Informe o User Name", AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe o nome do usuário", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Informe um email válido.")]
        public string Email { get; set; }        
    }
}