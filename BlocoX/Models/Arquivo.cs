using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
    public class Arquivo
    {
        public string Recibo { get; set; }
        public string Ecf { get; set; }
        public DateTime DataRefenrencia { get; set; }
        public DateTime DataHoraRecepcao { get; set; }
        public DateTime DataHoraProcessamento { get; set; }
        public int TipoRecepcaoCodigo { get; set; }
        public string TipoRecepcaoDescricao { get; set; }
        public int SituacaoProcessamentoCodigo { get; set; }
        public string SituacaoProcessamentoDescricao { get; set; }

        public Arquivo(string recibo, string ecf, DateTime dataRefenrencia, DateTime dataHoraRecepcao, DateTime dataHoraProcessamento, int tipoRecepcaoCodigo, string tipoRecepcaoDescricao, int situacaoProcessamentoCodigo, string situacaoProcessamentoDescricao)
        {
            Recibo = recibo;
            Ecf = ecf;
            DataRefenrencia = dataRefenrencia;
            DataHoraRecepcao = dataHoraRecepcao;
            DataHoraProcessamento = dataHoraProcessamento;
            TipoRecepcaoCodigo = tipoRecepcaoCodigo;
            TipoRecepcaoDescricao = tipoRecepcaoDescricao;
            SituacaoProcessamentoCodigo = situacaoProcessamentoCodigo;
            SituacaoProcessamentoDescricao = situacaoProcessamentoDescricao;
        }
    }
}
