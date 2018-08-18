using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlazorCodingNight.Shared
{
    public class Contato
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string TelefoneRes { get; set; }
        public string TelefoneCel { get; set; }
        public string Observacoes { get; set; }
    }
}
