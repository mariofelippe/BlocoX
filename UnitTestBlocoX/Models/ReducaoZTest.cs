using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlocoX.Models;
using BlocoX.Utils;
using System.Collections.Generic;

namespace UnitTestBlocoX.Models
{
    [TestClass]
    [TestCategory("Redução Z")]
    public class ReducaoZTest
    {
        [TestMethod]
        public void TestCalculaValorVendaBrutaDiaria()
        {
            Estabelecimento estabelecimento = new Estabelecimento("24123456");
            Ecf ecf = new Ecf("BE111610101110048745");
            PafEcf paf = new PafEcf("00112300001237");
            DadosReducao dadosReducao = new DadosReducao(DateTime.Now, DateTime.Now, 2, 4, 1, 10000.54M, 1M);
            Produto primeiroProduto = new Produto("Produto Test", "4714247187047", "0", "66019110", "2169997", 50.00M, "PC", 1440.10M, 0, 100M, 3140.54M);
            Produto segundoProduto = new Produto("Produto Test 2", "7896058593402", "0", "66019110", "2169997", 10.00M, "PC", 1210.10M, 0, 50M, 4450.00M);
            Produto terceiroProduto = new Produto("Produto Test 3", "7864234564531", "0", "66019110", "2169997", 1.00M, "PC", 0, 0, 0, 50.00M);

            TotalizadorParcial primeiroTotalizador = new TotalizadorParcial("01T1700", 1050.10M);
            TotalizadorParcial SegundoTotalizador = new TotalizadorParcial("F1", 50.00M);
            primeiroTotalizador.AdicionarProduto(primeiroProduto);
            primeiroTotalizador.AdicionarProduto(segundoProduto);
            SegundoTotalizador.AdicionarProduto(terceiroProduto);
            ReducaoZ reducaZ = new ReducaoZ(ecf, dadosReducao);
            reducaZ.AdicionarTotalizador(primeiroTotalizador);
            reducaZ.AdicionarTotalizador(SegundoTotalizador);
            Assert.AreEqual(10440.74M, reducaZ.CalculaValorVendaBrutaDiaria());
        }
    }
}
