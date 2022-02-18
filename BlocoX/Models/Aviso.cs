using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
    public class Aviso
    {
        private int codigo;
        private string descricao;

        public Aviso (int codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
        public int Codigo { get => codigo; set => codigo = value; }
        public string Descricao { get => descricao; set => descricao = value; }
    }
}
