using BSI.GestDoc.BusinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.BusinessLogic.BusinessException
{
    public class BusinessException: Exception
    {
       
        private Retorno _retorno { get; set; }
        public Retorno GetRetorno()
        {
            return _retorno;
        }

        public BusinessException(string mensagem) : base(mensagem) {
            _retorno = new Retorno();
            _retorno.Codigo = 0;
            _retorno.Mensagem = mensagem;
            _retorno.TipoErro = EnumTipoMensagem.Alerta;
        }

        public BusinessException(EnumTipoMensagem tipoErro, string mensagem) : base(mensagem)
        {
            _retorno = new Retorno();
            _retorno.Codigo = 0;
            _retorno.Mensagem = mensagem;
            _retorno.TipoErro = tipoErro;
        }

        public BusinessException(int codigo,  string mensagem) : base(mensagem)
        {
            _retorno = new Retorno();
            _retorno.Codigo = codigo;
            _retorno.Mensagem = mensagem;
            _retorno.TipoErro = EnumTipoMensagem.Alerta;
        }

        public BusinessException(int codigo, EnumTipoMensagem tipoErro, string mensagem) : base(mensagem)
        {
            _retorno = new Retorno();
            _retorno.Codigo = codigo;
            _retorno.Mensagem = mensagem;
            _retorno.TipoErro = tipoErro;
        }


    }
}
