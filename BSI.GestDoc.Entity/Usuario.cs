﻿using BSI.GestDoc.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperAttribute = Dapper.Contrib.Extensions;

namespace BSI.GestDoc.Entity
{
    [DapperAttribute.Table("Usuario")]
    public class Usuario
    {
        [DapperAttribute.Key]
        public Int64 UsuarioId { get; set; }
        public string UsuarioLogin { get; set; }
        public string UsuarioNome { get; set; }
        public string UsuarioEmail { get; set; }
        public string UsuarioSenha { get; set; }
        public bool UsuarioAtivo { get; set; }
        public int UsuPerfilId { get; set; }
        public int ClienteId { get; set; }
        public string AllowedOrigin { get; set; }


        #region Auxiliares
        [DapperAttribute.Write(false)]
        public string UsuarioAtivoDescricao
        {
            get { return UsuarioAtivo ? "Ativo" : "Inativo"; }
        }
        [DapperAttribute.Write(false)]
        public int StatusProcessamento { get; set; }
        [DapperAttribute.Write(false)]
        public string MensagemProcessamento { get; set; }

        [DapperAttribute.Write(false)]
        public Cliente Cliente { get; set; }
        [DapperAttribute.Write(false)]
        public UsuarioPerfil UsuarioPerfil { get; set; }

        [DapperAttribute.Write(false)]
        public ApplicationTypes ApplicationType { get { return ApplicationTypes.JavaScript; } }
        #endregion
    }
}
