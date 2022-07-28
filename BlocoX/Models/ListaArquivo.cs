using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
    public class ListaArquivo
    {
        public int SituacaoOperacaoCodigo { get; set; }
        public string SituacaoOperacaoDescricao { get; set; }
        public string Ie { get; set; }

        public List<Arquivo> Arquivos { get; set; } = new List<Arquivo>();

        public void AdicionarArquivo (Arquivo arquivo)
        {
            Arquivos.Add(arquivo);
        }
    }
}
