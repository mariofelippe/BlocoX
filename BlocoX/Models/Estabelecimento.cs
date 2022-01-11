using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
    public class Estabelecimento
    {
        private string ie;


        // Construtor
        public Estabelecimento(string inscricaoEstadual)
        {
            Ie = inscricaoEstadual;
        }


        public string Ie { get => ie; set => ie = value; }
    }
}
