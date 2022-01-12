using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlocoX.Models;
using System.Collections.Generic;

namespace UnitTestBlocoX.Models
{
    [TestClass]
    [TestCategory("Test Models")]
    public class TotalizadorParcialTest
    {
        [TestMethod]
        public void TestCalculaValorTotalizador()
        {
            Produto primeiroProduto = new Produto("Produto Test", "4714247187047", "0", "66019110", "2169997", 1.00M, "PC", 240.10M, 0, 10, 3000.54M);
            Produto segundoProduto = new Produto("Produto Test 2", "7896058593402", "0", "66019110", "2169997", 10.00M, "PC", 120.10M, 0, 10, 450.00M);
            List<Produto> listaProdutos = new List<Produto>();
            listaProdutos.Add(primeiroProduto);
            listaProdutos.Add(segundoProduto);
            TotalizadorParcial totalizador = new TotalizadorParcial("F1", 1000M,listaProdutos);
            Assert.AreEqual(3450.54M, totalizador.CalculaValorTotalizador());

        }
    }
}
