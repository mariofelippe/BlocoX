﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
    public class TotalizadorParcial
    {
        private string nome;
        private decimal valor;
        private List<Produto> produtos;

        public TotalizadorParcial(string nome, decimal valor)
        {
            Nome = nome;
            Valor = valor;
            Produtos = new List<Produto>();
        }
        public TotalizadorParcial( string nome, decimal valor, List<Produto> produtos)
        {
            Nome = nome;
            Valor = valor;
            Produtos = produtos;
        }

        public string Nome { get => nome; set => nome = value; }
        public decimal Valor { get => valor; set => valor = value; }
        public List<Produto> Produtos { get => produtos; set => produtos = value; }

        public void AdicionarProduto(Produto produto)
        {
            Produtos.Add(produto);
        }

        public decimal CalculaValorTotalizador()
        {
            decimal valorCalculado = 0;

            for (int i = 0; i < produtos.Count; i++)
            {
                valorCalculado += produtos[i].ValorLiquido;               
            }
            
            return valorCalculado;
        }

        public void AjustaValorTotalizador()
        {
            if(valor != CalculaValorTotalizador())
            {
                Valor = CalculaValorTotalizador();
            }
        }
    }
}
