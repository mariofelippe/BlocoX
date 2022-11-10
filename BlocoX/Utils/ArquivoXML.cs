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
        private static string nomeArquivo;

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
                nomeArquivo = arquivoXml;

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

            for (int i = 0; i < totalizadoresXml.Count; i++)
            {
                               
                XmlNodeList produtosTag = totalizadoresXml[i]["ProdutosServicos"].ChildNodes;
                List<Produto> produtosLista = new List<Produto>();

                for (int indice = 0; indice < produtosTag.Count; indice++)
                {
                    try
                    {

                        Produto produto = new Produto(produtosTag[indice]["Descricao"].InnerText, 
                            produtosTag[indice]["CodigoGTIN"].InnerText, 
                            produtosTag[indice]["CodigoCEST"].InnerText,
                            produtosTag[indice]["CodigoNCMSH"].InnerText,
                            produtosTag[indice]["CodigoProprio"].InnerText,
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
                        Console.WriteLine($"Erro ao adicionar o produto {produtosTag[indice]["Descricao"].InnerText} ao totalizador {totalizadoresXml[i]["Nome"].InnerText}!");
                        Console.WriteLine(e.Message);
                        
                       
                    }
                }

                TotalizadorParcial totalizadorParcial = new TotalizadorParcial(totalizadoresXml[i]["Nome"].InnerText,
                    decimal.Parse(totalizadoresXml[i]["Valor"].InnerText), 
                    produtosLista);

                totalizadores.Add(totalizadorParcial);
            }

            return totalizadores;
        }
         
        public static void SalvarArquivoXML(string nomeCaminho, string conteudoXML)
        {
            XmlDocument xmlArquivo = new XmlDocument();
            //xmlArquivo.PreserveWhitespace = true;
            xmlArquivo.LoadXml(conteudoXML);
            xmlArquivo.Save(nomeCaminho);
            Util.CompatactarXML(nomeCaminho);
            
        }

        public static PendenciaContribuinte GetPendenciaContribuinte(string conteudoXML)
        {

            PendenciaContribuinte pendenciaContribuinte = new PendenciaContribuinte();
            
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(conteudoXML);

            if(xml.SelectSingleNode("//SituacaoOperacaoCodigo").InnerText != "0")
            {
                Console.WriteLine(xml.SelectSingleNode("//SituacaoOperacaoDescricao").InnerText);
                return pendenciaContribuinte;
            }
            Estabelecimento estabelecimento = new Estabelecimento(xml.SelectSingleNode("//IE").InnerText);
            pendenciaContribuinte.DataInicioObrigacao = DateTime.Parse(xml.SelectSingleNode("//DataInicioObrigacao").InnerText);
            pendenciaContribuinte.Estabelecimento = estabelecimento;
            XmlNodeList ecfs = xml.SelectNodes("//ReducoesZ/Ecf");

            foreach(XmlNode node in ecfs)
            {
                Ecf ecf = new Ecf(node["NumeroFabricacaoEcf"].InnerText);
                PendenciaEcf pendenciaEcf = new PendenciaEcf(ecf,int.Parse(node["SituacaoPafEcfCodigo"].InnerText), node["SituacaoPafEcfDescricao"].InnerText, int.Parse(node["QuantidadePendencias"].InnerText), int.Parse(node["QuantidadeAvisos"].InnerText));
                if(node["QuantidadePendencias"].InnerText != "0")
                {
                    XmlNodeList listaPendencias = node["Pendencias"].ChildNodes;

                    for (int i = 0; i < listaPendencias.Count; i++)
                    {
                        Pendencia pendencia = new Pendencia(int.Parse(listaPendencias[i]["Codigo"].InnerText), listaPendencias[i]["Descricao"].InnerText, int.Parse(listaPendencias[i]["Quantidade"].InnerText));
                        pendenciaEcf.AdicionarPendencia(pendencia);

                    }
                }
                
                
                if(node.LastChild.Name == "Avisos")
                {
                    XmlNodeList listaAvisos = node["Avisos"].ChildNodes;
                    for(int i=0; i < listaAvisos.Count; i++)
                    {
                        Aviso aviso = new Aviso(int.Parse(listaAvisos[i]["Codigo"].InnerText), listaAvisos[i]["Descricao"].InnerText);
                        pendenciaEcf.AdicionarAviso(aviso);
                    }
                }
                pendenciaContribuinte.AdicionarPendenciaEcf(pendenciaEcf);
                
                
            }
            
            return pendenciaContribuinte;
           
        }

        public static ListaArquivo GetListaArquivo(string conteudoXML)
        {
            ListaArquivo listaArquivo = new ListaArquivo();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(conteudoXML);

            if (xml.SelectSingleNode("//SituacaoOperacaoCodigo").InnerText != "0")
            {
                Console.WriteLine(xml.SelectSingleNode("//SituacaoOperacaoDescricao").InnerText);
                return listaArquivo;
            }

            listaArquivo.SituacaoOperacaoCodigo = int.Parse(xml.SelectSingleNode("//SituacaoOperacaoCodigo").InnerText);
            listaArquivo.SituacaoOperacaoDescricao = xml.SelectSingleNode("//SituacaoOperacaoDescricao").InnerText;
            listaArquivo.Ie = xml.SelectSingleNode("//IE").InnerText;

            var nodes = xml.SelectNodes("//Arquivos//Arquivo");

            foreach (XmlNode node in nodes)
            {
                Arquivo arquivo = new Arquivo(node["Recibo"].InnerText, 
                    node["NumeroFabricacaoEcf"].InnerText,
                    DateTime.Parse(node["DataReferencia"].InnerText),
                    DateTime.Parse(node["DataHoraRecepcao"].InnerText),
                    DateTime.Parse(node["DataHoraProcessamento"].InnerText),
                    int.Parse(node["TipoRecepcaoCodigo"].InnerText),
                    node["TipoRecepcaoDescricao"].InnerText,
                    int.Parse(node["SituacaoProcessamentoCodigo"].InnerText),
                    node["SituacaoProcessamentoDescricao"].InnerText);
                
                listaArquivo.AdicionarArquivo(arquivo);
               
            }

            return listaArquivo;
        }
    }

}
