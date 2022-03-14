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

        public ReducaoZ (Ecf ecf, DadosReducao dadosReducao)
        {
            ECF = ecf;
            DadosReducao = dadosReducao;
            Totalizadores = new List<TotalizadorParcial>();
        }
        public ReducaoZ(Ecf ecf, DadosReducao dadosReducao, List<TotalizadorParcial> totalizadores)
        {
            ECF = ecf;
            DadosReducao = dadosReducao;
            Totalizadores = totalizadores;
        }

        public Ecf ECF { get => ecf; set => ecf = value; }
        public DadosReducao DadosReducao { get => dadosReducao; set => dadosReducao = value; }
        public List<TotalizadorParcial> Totalizadores { get => totalizadores; set => totalizadores = value; }

        public void AdicionarTotalizador(TotalizadorParcial totalizador)
        {
            totalizadores.Add(totalizador);
        }
        public decimal CalculaValorVendaBrutaDiaria()
        {
            decimal vendaBrutaCalculada = 0;

            foreach(TotalizadorParcial totalizador in totalizadores)
            {
                for (int i = 0; i < totalizador.Produtos.Count; i++)
                {
                  vendaBrutaCalculada += totalizador.Produtos[i].ValorDesconto;
                  vendaBrutaCalculada += totalizador.Produtos[i].ValorCancelamento;
                  vendaBrutaCalculada += totalizador.Produtos[i].ValorLiquido;
                }
            }

            return vendaBrutaCalculada;
        }

        public void AjustarVarlorVendaBrutaDiaria()
        {
            if(dadosReducao.VendaBrutaDiaria != CalculaValorVendaBrutaDiaria())
            {
                dadosReducao.VendaBrutaDiaria = CalculaValorVendaBrutaDiaria();
            }
        }
    
        public void AjustaValoresTotalizadores()
        {
            foreach(TotalizadorParcial totalizador in totalizadores)
            {
                totalizador.AjustaValorTotalizador();
            }
        }

        public string GeraNomeReducaoZ()
        {
            string nome = "Reducao Z " + dadosReducao.DataReferencia.ToString().Split(' ')[0].Replace("/","");
            return nome;
        }

        public void AjustarSequenciaTotalizador()
        {
            int seq = 1;
            foreach (TotalizadorParcial totalizador in totalizadores)
            {
                if(totalizador.Nome.Contains("T")){
                    totalizador.Nome = seq.ToString().PadLeft(2, '0') + "T" +totalizador.Nome.Split('T')[1];
                    seq += 1;
                }
            }
        }
    }
}
