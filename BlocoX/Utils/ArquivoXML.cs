using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BlocoX.Models;


namespace BlocoX.Utils
{
    public class ArquivoXML
    {

        private static XmlDocument xml = new XmlDocument();

        public static bool ValidaXMLReducaoZ(string arquivo)
        {
           
            if (!System.IO.File.Exists(arquivo) || !(System.IO.Path.GetExtension(arquivo) == ".xml"))
            {                
                return false;
            }

            XmlDocument xmlArquivo = new XmlDocument();

            xmlArquivo.Load(arquivo);
            if (xmlArquivo.DocumentElement.Name != "ReducaoZ")
            {
                return false;
            }

            return true;
        }

        public static void CarregarXML(string arquivoXml)
        {

            try
            {
                xml.Load(arquivoXml);
                Console.WriteLine($"arquivo aberto {arquivoXml}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao tentar abrir o arquivo {arquivoXml}!");
                Console.WriteLine(e);
            }
        }

        public static Estabelecimento GetEstabelecimento()
        {

            string inscricaoEstadual = "";
            inscricaoEstadual = xml.SelectSingleNode("//Ie").InnerText;
            Estabelecimento estabelecimento = new Estabelecimento(inscricaoEstadual);

            return estabelecimento;
        }


        public static PafEcf GetPafEcf()
        {
            string numeroCredenciamento = "";
            numeroCredenciamento = xml.SelectSingleNode("//NumeroCredenciamento").InnerText;
            PafEcf paf = new PafEcf(numeroCredenciamento);
            return paf;
        }

        public static Ecf GetEcf()
        {
            string numeroFabricacao = "";
            numeroFabricacao = xml.SelectSingleNode("//NumeroFabricacao").InnerText;
            Ecf ecf = new Ecf(numeroFabricacao);
            return ecf;
        }

        public static DadosReducao GetDadosReducao()
        {
            try
            {
                DateTime dataRefencia = Util.StrToDate(xml.SelectSingleNode("//DataReferencia").InnerText);
                DateTime dataHoraEmissap = Util.StrToDate(xml.SelectSingleNode("//DataHoraEmissao").InnerText);
                int crz = Util.StrIntToClear(xml.SelectSingleNode("//CRZ").InnerText);
                int coo = Util.StrIntToClear(xml.SelectSingleNode("//COO").InnerText);
                int cro = Util.StrIntToClear(xml.SelectSingleNode("//CRO").InnerText);
                decimal venBrutaDiaria = Util.StrDecimalToClear(xml.SelectSingleNode("//VendaBrutaDiaria").InnerText) / 100;
                decimal gt = Util.StrDecimalToClear(xml.SelectSingleNode("//GT").InnerText) / 100;

                DadosReducao dadosReducao = new DadosReducao(dataRefencia, dataHoraEmissap, crz, coo, cro, venBrutaDiaria, gt);
                return dadosReducao;
            } catch (System.FormatException  e){
                Console.WriteLine("Não foi possível coletar os dados da Redução!");
                Console.WriteLine(e);
                DadosReducao dadosReducao = new DadosReducao(DateTime.Now, DateTime.Now,0,0,0,0,0);
                return dadosReducao;
            }
            
        }

        public static List<TotalizadorParcial> GetTotalizadores()
        {
            List<TotalizadorParcial> totalizadores = new List<TotalizadorParcial>();
            XmlNodeList totalizadoresXml = xml.SelectNodes("//TotalizadorParcial");

        // CAPTURAR AS EXCEÇÔES ESPECIFICAS.

            for (int i = 0; i < totalizadoresXml.Count; i++)
            {
                               
                XmlNodeList produtosTag = totalizadoresXml[i]["ProdutosServicos"].ChildNodes;
                List<Produto> produtosLista = new List<Produto>();

                for (int indice = 0; indice < produtosTag.Count; indice++)
                {
                    try
                    {
                        string descricao = produtosTag[indice]["Descricao"].InnerText;
                        string codigoGTIN = produtosTag[indice]["CodigoGTIN"].InnerText;
                        string codigoCEST = produtosTag[indice]["CodigoCEST"].InnerText;
                        string codigProprio = produtosTag[indice]["CodigoProprio"].InnerText;
                        Produto produto = new Produto(descricao, codigoGTIN, codigoCEST,
                            produtosTag[indice]["CodigoNCMSH"].InnerText,
                            codigProprio,
                            decimal.Parse(produtosTag[indice]["Quantidade"].InnerText),
                            produtosTag[indice]["Unidade"].InnerText,
                            decimal.Parse(produtosTag[indice]["ValorDesconto"].InnerText),
                            decimal.Parse(produtosTag[indice]["ValorAcrescimo"].InnerText),
                            decimal.Parse(produtosTag[indice]["ValorCancelamento"].InnerText),
                            decimal.Parse(produtosTag[indice]["ValorTotalLiquido"].InnerText)
                            );
                       
                        produtosLista.Add(produto);
                    } catch (System.FormatException e)
                    {
                        Console.WriteLine($"Erro ao adicionar o produto ao totalizador!");
                        Console.WriteLine(e);
                    }
                }

                TotalizadorParcial totalizadorParcial = new TotalizadorParcial(totalizadoresXml[i]["Nome"].InnerText,
                    decimal.Parse(totalizadoresXml[i]["Valor"].InnerText), 
                    produtosLista);

                totalizadores.Add(totalizadorParcial);
            }

            return totalizadores;
        }
        
    }

}
