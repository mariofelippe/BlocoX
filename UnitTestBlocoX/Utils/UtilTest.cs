using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlocoX.Utils;

namespace UnitTestBlocoX
{
    [TestClass]
    [TestCategory("Utils Tests")]
    public class UtilTest
    {
        [TestMethod]
        public void TestStrToDate()
        {
            string str = "2021-01-01";          
            Assert.AreEqual(Util.StrToDate(str), DateTime.Parse(str));
        }

        [TestMethod]
        public void TestStrIntToClear()
        {
            string str = "1#f2G?3";
            Assert.AreEqual(123, Util.StrIntToClear(str));
        }

        [TestMethod]
        public void TestStrDecimalToClear()
        {
            string valor = "1fçsdf2fa3f4f56789asd";
            decimal valorDecimal = 1234567.89M;
            Assert.AreEqual(valorDecimal, Util.StrDecimalToClear(valor)/100);
        }

        [TestMethod]
        public void TestRemoverPontoVirgula()
        {
            string str = "2.545,20";
            Assert.AreEqual("254520", Util.RemoverPontoVirgula(str));
        }

        [TestMethod]
        public void TestAdicionaZeroEsqueda()
        {
            string str = "254520";
            Assert.AreEqual("000000000000254520", Util.AdiconaZeroEsqueda(18, str));
        }
    }
}
