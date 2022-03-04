using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using BlocoX.Utils;
using BlocoX.Models;
using System.IO;
using BlocoX.Services;



namespace BlocoX
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Apoio Bloco X";
            Config config = new Config();
            ServicoBlocoX servico = new ServicoBlocoX();
            string op;         
            
            while (true)
            {

                Console.WriteLine("*****************************************************************");
                Console.WriteLine("                       Apoio Bloco X");
                Console.WriteLine("*****************************************************************");
                Console.WriteLine();
                Console.WriteLine("Opções:");
                Console.WriteLine("1 - Enviar XMLs Redução Z.");
                Console.WriteLine("2 - Consultar Recibos.");
                Console.WriteLine("3 - Cancelar XMLs Redução Z.");
                Console.WriteLine("4 - Baixar XMLs Redução Z.");
                Console.WriteLine("5 - Consultar Pendências Contribuinte.");
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
                        Console.WriteLine("\nEnviando XML...\n");
                        Retorno retorno = servico.EnviaXMLReducaoZ(strXML, reducaoZ.GeraNomeReducaoZ());
                        Console.WriteLine("Recibo: " + retorno.Recibo);
                        Console.WriteLine("Codigo Processamento: " + retorno.CodigoProcessamento);
                        Console.WriteLine("Descrição: " + retorno.Descricao);
                        Console.WriteLine("Mensagem: " + retorno.Mensagem);
                        Util.SalvaLogRetorno($@"{config.PathLogs}\EnvioXmlReducaoZ.csv", $"{estabelecimento.Ie};{reducaoZ.ECF.NumeroFabricacao};{reducaoZ.DadosReducao.CRZ};{retorno.Recibo};{retorno.CodigoProcessamento};{retorno.Descricao};{retorno.Mensagem}");
                        Thread.Sleep(config.TempoEsperaEnvio);
                    }

                    Console.WriteLine($"\n{arquivos.Length} arquivo(s) processado(s) em {DateTime.Now - dataInicial}.");
                }

                if (op == "2")
                {
                    config.CarregaParametrosConfig();
                    Console.WriteLine("\nConsultando...");
                   
                    string listaConsulta = config.ListaConsulta;                                       
                    using (StreamReader linha = new StreamReader(config.ListaConsulta))
                    {
                        string recibo;
                        while ((recibo = linha.ReadLine()) != null)
                        {

                            Console.WriteLine($"Consultando o recibo: {recibo}.\n");
                            Retorno retorno = servico.ConsultarProcessamentoArquivo(recibo);
                            Console.WriteLine("Recibo: " + retorno.Recibo);
                            Console.WriteLine("Codigo Processamento: " + retorno.CodigoProcessamento);
                            Console.WriteLine("Descrição: " + retorno.Descricao);
                            Util.SalvaLogRetorno($@"{config.PathLogs}\ConsultaRecibo.csv", $"{retorno.Recibo};{retorno.CodigoProcessamento};{retorno.Descricao}");
                            Console.WriteLine("-------------------------------------------------------------------------------------------\n");
                            Thread.Sleep(config.TempoEsperaConsulta);
 
                        }
                    }
                   
         
                }

                if (op == "3")
                {
                    config.CarregaParametrosConfig();
                    Console.WriteLine("\nCancelando XML...");

                    string[] listaRecibo = File.ReadAllLines(config.ListaCancelamento);
                    
                    for (int i = 0; i < listaRecibo.Length; i++)
                    {
                        Console.WriteLine($"Cancelando a redução do recibi {listaRecibo[i]}...\n");
                        string xml = Xml.XmlCancealmentoReducaoZ(listaRecibo[i], config.MotivoCancelamento);
                        xml = Xml.AssinarXML(xml, config.GetCertificado());
                        Retorno retorno = servico.CancelaReducaoZ(xml);                        
                        Console.WriteLine($"Recibo: {retorno.Recibo}");
                        Console.WriteLine($"Codigo Processamento: {retorno.CodigoProcessamento}");
                        Console.WriteLine($"Descrição: {retorno.Descricao}");
                        Console.WriteLine($"Mensagem: {retorno.Mensagem}");
                        Console.WriteLine("-------------------------------------------------------------------------------------------\n");
                        Util.SalvaLogRetorno($@"{config.PathLogs}\Cancelamento.csv", $"{retorno.Recibo};{retorno.CodigoProcessamento};{retorno.Descricao};{retorno.Mensagem}");

                    }
                    
                }

                if (op == "4")
                {
                    config.CarregaParametrosConfig();
                    Console.WriteLine("Baixando arquivo...\n");
                   
                    string[] recibos = File.ReadAllLines(config.ListaDownloadArquivo);
                    for (int i = 0; i < recibos.Length; i++)
                    {
                        Console.WriteLine($"Baixando o XML do recibo {recibos[i]}...");
                        string xml = Xml.XmlDownloadReducaoZ(recibos[i]);
                        xml = Xml.AssinarXML(xml, config.GetCertificado());
                        Util.SalvarArquivoBase64(servico.DownloadXMLReducaoZ(xml));
                        
                        if (i < recibos.Length - 1)
                        {
                           
                            Thread.Sleep(config.TempoEsperaDownload);
                        }
                    }
                    
                }

                if (op == "5")
                {
                    DateTime data = DateTime.Now;
                    config.CarregaParametrosConfig();
                    string[] listaIEs;
                    if (!File.Exists(config.ListaConsultarPendenciasContribuinte))
                    {
                        Console.WriteLine($"O Arquivo {config.ListaConsultarPendenciasContribuinte} não foi localizado");
                        continue;
                    }
                    listaIEs = File.ReadAllLines(config.ListaConsultarPendenciasContribuinte);
                    if(listaIEs.Length == 0)
                    {
                        continue;
                    }
                    
                   
                    using(FileStream fs = new FileStream($"PendenciasContribuinte_{data.ToString("ddMMyyyy_HHmmss")}.csv", FileMode.Create))
                    {
                       
                        using (StreamWriter writer = new StreamWriter(fs, Encoding.Default))
                        {
                            //Escreve o cabeçalho no arquivo,
                            writer.WriteLine("Inscrição Estadual;Data do início da obrigação;N° de Fabricação do ECF;Tipo;Código da pendência;Quantidade;Descrição");

                            foreach (string ie in listaIEs)
                            {
                                Console.WriteLine($"Consultando as pendências do Inscrição {ie}.");
                                string conteudoXml = Xml.XmlConsultaPendenciaContribuinte(ie);
                                conteudoXml = Xml.AssinarXML(conteudoXml, config.GetCertificado());
                                PendenciaContribuinte contribuinte = ArquivoXML.GetPendenciaContribuinte(servico.ConsultaPendenciaContribuinte(conteudoXml));
                                                         
                                foreach (PendenciaEcf pendenciaEcf in contribuinte.PendenciasEcfs)
                                {
                                    foreach (Pendencia pendencia in pendenciaEcf.Pendencias)
                                    {
                                        writer.WriteLine($"{contribuinte.Estabelecimento.Ie};{contribuinte.DataInicioObrigacao};{pendenciaEcf.Ecf.NumeroFabricacao};Pendência;{pendencia.Codigo};{pendencia.Quantidade};{pendencia.Descricao}");
                                    }

                                    foreach (Aviso aviso in pendenciaEcf.Avisos)
                                    {
                                        writer.WriteLine($"{contribuinte.Estabelecimento.Ie};{contribuinte.DataInicioObrigacao};{pendenciaEcf.Ecf.NumeroFabricacao};Aviso;{aviso.Codigo};;{aviso.Descricao}");

                                    }
                                }

                            }
                            
                        }

                    }
                  


                }
            }



            
           
        }
    }
}
