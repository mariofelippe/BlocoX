using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
    public class PafEcf
    {
        private string numeroCredenciamento;


        public PafEcf(string numeroCredenciamento)
        {
            NumeroCredenciamento = numeroCredenciamento;
        }

        public string NumeroCredenciamento { get => numeroCredenciamento; set => numeroCredenciamento = value; }
    }
}
