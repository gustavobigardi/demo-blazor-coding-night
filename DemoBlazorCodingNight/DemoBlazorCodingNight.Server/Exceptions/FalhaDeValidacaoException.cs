using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlazorCodingNight.Server.Exceptions
{
    public class FalhaDeValidacaoException : Exception
    {
        public List<string> Erros { get; private set; }

        public FalhaDeValidacaoException(string mensagem, List<string> erros) : base(mensagem)
        {
            Erros = erros;
        }
    }
}
