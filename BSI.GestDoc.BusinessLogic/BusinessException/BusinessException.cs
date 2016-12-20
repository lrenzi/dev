using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.BusinessLogic.BusinessException
{
    public class BusinessException: Exception
    {
        public int Codigo { get; set; }
        public EnumTipoErro TipoErro { get; set; }

        public BusinessException(string mensagem) : base(mensagem) {
            Codigo = 0;
        }

        public BusinessException(EnumTipoErro tipoErro, string mensagem) : base(mensagem)
        {
            Codigo = 0;
            this.TipoErro = tipoErro;
        }

        public BusinessException(int codigo,  string mensagem) : base(mensagem)
        {
            Codigo = codigo;
        }

        public BusinessException(int codigo, EnumTipoErro tipoErro, string mensagem) : base(mensagem)
        {
            Codigo = codigo;
            this.TipoErro = tipoErro;
        }

        public enum EnumTipoErro
        {
            Alerta = 1,
            Pergunta = 2
        }

    }
}
