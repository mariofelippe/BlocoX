using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
    public class Pendencia
    {
        private int codigo;
        private string descricao;
        private int quantidade;

        public Pendencia(int codigo, string descricao, int quantidade)
        {
            Codigo = codigo;
            Descricao = descricao;
            Quantidade = quantidade;
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public int Quantidade { get => quantidade; set => quantidade = value; }
    }
}
