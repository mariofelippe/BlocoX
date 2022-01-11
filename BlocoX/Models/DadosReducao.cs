using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
    public class DadosReducao
    {

        private DateTime dataReferencia;
        private DateTime dataHoraEmissao;
        private int crz;
        private int coo;
        private int cro;
        private decimal vendaBrutaDiaria;
        private decimal gt;

        public DadosReducao(DateTime dataReferencia, DateTime dataHoraEmissao, int crz, int coo, int cro, decimal vendaBrutaDiaria, decimal gt)
        {
            DataReferencia = dataReferencia;
            DataHoraEmissao = dataHoraEmissao;
            CRZ = crz;
            COO = coo;
            CRO = cro;
            VendaBrutaDiaria = vendaBrutaDiaria;
            GT = gt;
        }

        public DateTime DataReferencia { get => dataReferencia; set => dataReferencia = value; }
        public DateTime DataHoraEmissao { get => dataHoraEmissao; set => dataHoraEmissao = value; }
        public int CRZ { get => crz; set => crz = value; }
        public int COO { get => coo; set => coo = value; }
        public int CRO { get => cro; set => cro = value; }
        public decimal VendaBrutaDiaria { get => vendaBrutaDiaria; set => vendaBrutaDiaria = value; }
        public decimal GT { get => gt; set => gt = value; }
    }
}
