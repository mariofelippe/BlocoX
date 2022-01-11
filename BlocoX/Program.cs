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
                    if (!File.Exists(listaXML))
                    {
                        Console.WriteLine($"O arquivo {listaXML} não foi localizado!\n");
                        continue;
                    }
                    string[] arquivos = File.ReadAllLines(listaXML);

                    for (int i = 0; i < arquivos.Length; i++)
                    {
                         //Console.WriteLine(arquivos[i]);
                        if (!File.Exists(arquivos[i]))
                        {
                            Console.WriteLine($"Arquivo \"{arquivos[i]}\" localizado!");
                            continue;
                        }
                        if (!ArquivoXML.ValidaXMLReducaoZ(arquivos[i]))
                        {
                            Console.WriteLine($"Arquivo {arquivos[i]} inválido!");
                            continue;
                        }
                        ArquivoXML.CarregarXML(arquivos[i]);
                        List<TotalizadorParcial> totalizadores = ArquivoXML.GetTotalizadores();
                        Ecf ecf = ArquivoXML.GetEcf();
                        Estabelecimento estabelecimento = ArquivoXML.GetEstabelecimento();
                        PafEcf paf = ArquivoXML.GetPafEcf();
                        DadosReducao dadosReducao = ArquivoXML.GetDadosReducao();
                    }

                }
            }



            
            Console.ReadKey();
        }
    }
}
