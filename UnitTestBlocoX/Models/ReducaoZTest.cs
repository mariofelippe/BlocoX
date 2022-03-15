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
        [TestMethod]
        public void TestAjustarVarlorVendaBrutaDiaria()
        {
            Estabelecimento estabelecimento = new Estabelecimento("24123456");
            Ecf ecf = new Ecf("BE111610101110048745");
            PafEcf paf = new PafEcf("00112300001237");
            DadosReducao dadosReducao = new DadosReducao(DateTime.Now, DateTime.Now, 2, 4, 1, 10000.54M, 1M);
            Produto produto = new Produto("Produto Test", "4714247187047", "0", "66019110", "2169997", 50.00M, "PC", 140.10M, 0, 100M, 1140.54M);
           
            TotalizadorParcial totalizador = new TotalizadorParcial("F1", 50.00M);
            totalizador.AdicionarProduto(produto);
            ReducaoZ reducaZ = new ReducaoZ(ecf, dadosReducao);
            reducaZ.AdicionarTotalizador(totalizador);
            reducaZ.AjustarVarlorVendaBrutaDiaria();
            Assert.AreEqual(1380.64M, reducaZ.DadosReducao.VendaBrutaDiaria);
           
        }
        [TestMethod]
        public void TesteGeraNomeReducaoZ()
        {
            Estabelecimento estabelecimento = new Estabelecimento("24123456");
            Ecf ecf = new Ecf("BE111610101110048745");
            PafEcf paf = new PafEcf("00112300001237");
            DadosReducao dadosReducao = new DadosReducao(DateTime.Parse("2022-02-01"), DateTime.Now, 2, 4, 1, 10000.54M, 1M);
            Produto produto = new Produto("Produto Test", "4714247187047", "0", "66019110", "2169997", 50.00M, "PC", 140.10M, 0, 100M, 1140.54M);

            TotalizadorParcial totalizador = new TotalizadorParcial("F1", 50.00M);
            totalizador.AdicionarProduto(produto);
            ReducaoZ reducaZ = new ReducaoZ(ecf, dadosReducao);
            reducaZ.AdicionarTotalizador(totalizador);
            reducaZ.AjustarVarlorVendaBrutaDiaria();
            Assert.AreEqual("Reducao Z 01022022", reducaZ.GeraNomeReducaoZ());
        }
        [TestMethod]
        public void TestAjustarSequenciaTotalizador()
        {
            Estabelecimento estabelecimento = new Estabelecimento("24123456");
            Ecf ecf = new Ecf("BE111610101110048745");
            PafEcf paf = new PafEcf("00112300001237");
            DadosReducao dadosReducao = new DadosReducao(DateTime.Parse("2022-02-01"), DateTime.Now, 2, 4, 1, 10000.54M, 1M);
            Produto produto = new Produto("Produto Test", "4714247187047", "0", "66019110", "2169997", 50.00M, "PC", 140.10M, 0, 100M, 1140.54M);
            Produto produto2 = new Produto("Produto Test 2", "7896002304771", "0", "66015510", "4664195", 5.00M, "PC", 10.10M, 0, 1M, 21.44M);
            
            TotalizadorParcial totalizador = new TotalizadorParcial("05T1700", 50.00M);
            totalizador.AdicionarProduto(produto);
            TotalizadorParcial totalizador2 = new TotalizadorParcial("01T1200", 5.00M);
            totalizador.AdicionarProduto(produto2);
            ReducaoZ reducaZ = new ReducaoZ(ecf, dadosReducao);
            reducaZ.AdicionarTotalizador(totalizador);
            reducaZ.AdicionarTotalizador(totalizador2);
            reducaZ.AjustarSequenciaTotalizador();
            Assert.AreEqual("01T1700", reducaZ.Totalizadores[0].Nome);
            Assert.AreEqual("02T1200", reducaZ.Totalizadores[1].Nome);
        }
    }
}
