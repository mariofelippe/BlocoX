using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
   public class Ecf
    {
        private string numeroFabricacao;

        public Ecf (string numeroFabricacao)
        {
            NumeroFabricacao = numeroFabricacao;
        }

        public string NumeroFabricacao { get => numeroFabricacao; set => numeroFabricacao = value; }
        

        
    }
}
