using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.GestDoc.BusinessLogic.BusinessException
{
    public class BusinessException: Exception
    {
        public BusinessException(string mensagem) : base(mensagem) { }
    }
}
