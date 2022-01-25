using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Services
{
    public class Retorno
    {
        private string recibo;
        private int codigoProcessamento;
        private string descricao;
        private string mensagem;

        public string Recibo { get => recibo; set => recibo = value; }
        public int CodigoProcessamento { get => codigoProcessamento; set => codigoProcessamento = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public string Mensagem { get => mensagem; set => mensagem = value; }
        
    }
}
