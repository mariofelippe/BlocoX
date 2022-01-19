using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using BlocoX.Utils;
using BlocoX.Models;
using System.IO;



namespace BlocoX
{
    class Program
    {
        static void Main(string[] args)
        {

            Config config = new Config();

            string op;

            while (true)
            {

                Console.WriteLine("*****************************************************************");
                Console.WriteLine("                       Apoio Bloco X");
                Console.WriteLine("*****************************************************************");
                Console.WriteLine();
                Console.WriteLine("Opções:");
                Console.WriteLine("1 - Processar XMLs.");
                Console.WriteLine("99 - Sair.");
                Console.WriteLine();
                Console.Write("Opção: ");
                op = Console.ReadLine();

                if(op == "99")
                {
                    break;
                }

                if(op == "1")
                {
                   
                    config.CarregaParametrosConfig();
                    string listaXML = config.ListaXML;
                    Console.WriteLine("\nProcessando arquivos...\n");
                    if (!File.Exists(listaXML))
                    {
                        Console.WriteLine($"O arquivo {listaXML} não foi localizado!\n");
                        continue;
                    }
                    string[] arquivos = File.ReadAllLines(listaXML);
                    DateTime dataInicial = DateTime.Now;
                    for (int i = 0; i < arquivos.Length; i++)
                    {
                        Console.WriteLine("\n-------------------------------------------------------------------------------------------------");
                        //Console.WriteLine(arquivos[i]);
                        if (!File.Exists(arquivos[i]))
                        {
                            Console.WriteLine($"Arquivo \"{arquivos[i]}\" não localizado!");
                            continue;
                        }
                        if (!ArquivoXML.ValidaXMLReducaoZ(arquivos[i]))
                        {
                            Console.WriteLine($"Arquivo {arquivos[i]} inválido!");
                            continue;
                        }
                        Console.WriteLine($"\nProcessnado arquivo {arquivos[i]}...\n");
                        ArquivoXML.CarregarXML(arquivos[i]);
                        PafEcf paf = ArquivoXML.GetPafEcf();
                        Ecf ecf = ArquivoXML.GetEcf();
                        Estabelecimento estabelecimento = ArquivoXML.GetEstabelecimento();
                        DadosReducao dadosReducao = ArquivoXML.GetDadosReducao();
                        List<TotalizadorParcial> totalizadores = ArquivoXML.GetTotalizadores();
                        

                        Console.WriteLine($"Credenciamento: {paf.NumeroCredenciamento}");
                        Console.WriteLine($"IE: {estabelecimento.Ie}");
                        Console.WriteLine($"Numero de Fabricação: {ecf.NumeroFabricacao}");
                        Console.WriteLine($"Data Referência: {dadosReducao.DataReferencia}");
                        Console.WriteLine($"Emissão: {dadosReducao.DataHoraEmissao}");
                        Console.WriteLine($"CRZ: {dadosReducao.CRZ}");
                        Console.WriteLine($"COO: {dadosReducao.COO}");
                        Console.WriteLine($"CRZ: {dadosReducao.CRO}");
                        Console.WriteLine($"GT: {dadosReducao.GT}");
                        Console.WriteLine($"Venda Bruta: {dadosReducao.VendaBrutaDiaria}\n");
                        Console.WriteLine("Totalizadores:");
                        foreach(TotalizadorParcial totalizador in totalizadores)
                        {
                            Console.WriteLine($"Nome: {totalizador.Nome}");
                            Console.WriteLine($"Valor: {totalizador.Valor}");

                            if(config.AjustaValorTotalizador && totalizador.Valor != totalizador.CalculaValorTotalizador())
                            {
                                Console.WriteLine($"O valor do totalizador está divergênte. Ajustando para {totalizador.CalculaValorTotalizador()}");
                                totalizador.AjustaValorTotalizador();
                            }
                        }

                        if (config.AjustaCredenciamento && paf.NumeroCredenciamento != config.Credenciamento)
                        {
                            Console.WriteLine($"Ajustando o Credenciamento de {paf.NumeroCredenciamento} para {config.Credenciamento}");
                            paf.NumeroCredenciamento = config.Credenciamento;
                        }
                        ReducaoZ reducaoZ = new ReducaoZ(ecf, dadosReducao, totalizadores);
                        if (config.AjustaVendaBrutaDiaria && reducaoZ.DadosReducao.VendaBrutaDiaria != reducaoZ.CalculaValorVendaBrutaDiaria())
                        {
                            Console.WriteLine($"Corrigindo o valor da venda bruta para {reducaoZ.CalculaValorVendaBrutaDiaria()}");
                            reducaoZ.AjustarVarlorVendaBrutaDiaria();
                        }

                        string strXML = Xml.XmlReducaoZ(estabelecimento, paf, reducaoZ);

                        strXML = Xml.AssinarXML(strXML,config.GetCertificado());
                        ArquivoXML.SalvarArquivoXML(arquivos[i], strXML);
                    }

                    Console.WriteLine($"\n{arquivos.Length} arquivo(s) processado(s) em {DateTime.Now - dataInicial}.");
                }
            }



            
           
        }
    }
}
