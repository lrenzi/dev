﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.Entity
{
    public class Usuario
    {
        public Int64 UsuarioId { get; set; }
        public string UsuarioLogin { get; set; }
        public string UsuarioNome { get; set; }
        public string UsuarioEmail { get; set; }
        public string UsuarioSenha { get; set; }
        public bool UsuarioAtivo { get; set; }
        public int UsuPerfilId { get; set; }
        public int ClienteId { get; set; }
    }
}