using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
   public class ReducaoZ
    {

        private Ecf ecf;
        private DadosReducao dadosReducao;
        private List<TotalizadorParcial> totalizadores;

        public ReducaoZ(Ecf ecf, DadosReducao dadosReducao, List<TotalizadorParcial> totalizadores)
        {
            ECF = ecf;
            DadosReducao = dadosReducao;
            Totalizadores = totalizadores;
        }

        public Ecf ECF { get => ecf; set => ecf = value; }
        public DadosReducao DadosReducao { get => dadosReducao; set => dadosReducao = value; }
        public List<TotalizadorParcial> Totalizadores { get => totalizadores; set => totalizadores = value; }

    }
}
