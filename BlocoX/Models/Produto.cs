using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
   public class Produto
    {
        private string descricao;
        private string codigoGTIN;
        private string codigoCEST;
        private string codigoNCM;
        private string codigoProprio;
        private decimal quantidade;
        private string unidade;
        private decimal valorDesconto;
        private decimal valorAcrescimo;
        private decimal valorCancelamento;
        private decimal valorLiquido;

        public Produto(string descricao, string codigoGTIN, string codigoCEST, string codigoNCM, string codigoProprio, decimal quantidade, string unidade, decimal valorDesconto, decimal valorAcrescimo, decimal valorCancelamento, decimal valorLiquido)
        {
            Descricao = descricao;
            CodigoGTIN = codigoGTIN;
            CodigoCEST = codigoCEST;
            CodigoNCM = codigoNCM;
            CodigoProprio = codigoProprio;
            Quantidade = quantidade;
            Unidade = unidade;
            ValorDesconto = valorDesconto;
            ValorAcrescimo = valorAcrescimo;
            ValorCancelamento = valorCancelamento;
            ValorLiquido = valorLiquido;
        }

        public string Descricao { get => descricao; set => descricao = value; }
        public string CodigoGTIN { get => codigoGTIN; set => codigoGTIN = value; }
        public string CodigoCEST { get => codigoCEST; set => codigoCEST = value; }
        public string CodigoNCM { get => codigoNCM; set => codigoNCM = value; }
        public string CodigoProprio { get => codigoProprio; set => codigoProprio = value; }
        public decimal Quantidade { get => quantidade; set => quantidade = value; }
        public string Unidade { get => unidade; set => unidade = value; }
        public decimal ValorDesconto { get => valorDesconto; set => valorDesconto = value; }
        public decimal ValorAcrescimo { get => valorAcrescimo; set => valorAcrescimo = value; }
        public decimal ValorCancelamento { get => valorCancelamento; set => valorCancelamento = value; }
        public decimal ValorLiquido { get => valorLiquido; set => valorLiquido = value; }
    }
}
