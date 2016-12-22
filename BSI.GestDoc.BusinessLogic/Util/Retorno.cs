﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.BusinessLogic.Util
{
    public class Retorno
    {
        public EnumTipoMensagem TipoErro { get; set; }
        public int Codigo { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }
    }

    public enum EnumTipoMensagem
    {
        Sucesso = 1,
        Alerta = 2,
        Pergunta = 3
    }
}
