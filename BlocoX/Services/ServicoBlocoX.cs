using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlocoX.Utils;
using System.Xml;
using System.IO;
using System.IO.Compression;


namespace BlocoX.Services
{
    public class ServicoBlocoX
    {
        
        private SefazSC.BlocoXSoapClient client = new SefazSC.BlocoXSoapClient();
        private Config config = new Config();
        public Retorno ConsultarProcessamentoArquivo(string recibo)
        {
            XmlDocument xmlRetorno = new XmlDocument();
            Retorno retorno = new Retorno();
            try
            {
                string pXml = Xml.AssinarXML(Xml.XmlConsultarProcessmentoArquivo(recibo), config.GetCertificado());
                string xml = client.ConsultarProcessamentoArquivo(pXml);
                xmlRetorno.LoadXml(xml);                    
                retorno.Recibo = xmlRetorno.SelectSingleNode("//Recibo").InnerText;
                if (xmlRetorno.SelectSingleNode("//SituacaoOperacaoCodigo").InnerText == "3")
                {
                    retorno.CodigoProcessamento = 2;
                    retorno.Descricao = xmlRetorno.SelectSingleNode("//SituacaoOperacaoDescricao").InnerText;
                }
            
                else
                {
                    retorno.CodigoProcessamento = int.Parse(xmlRetorno.SelectSingleNode("//SituacaoProcessamentoCodigo").InnerText);
                    retorno.Descricao = xmlRetorno.SelectSingleNode("//SituacaoProcessamentoDescricao").InnerText;
                }

            } catch (System.ServiceModel.EndpointNotFoundException e)
            {
                Console.WriteLine("Não foi possível fazer a requisição! Verifique o endereço ou a conexão!");
                Console.WriteLine(e.Message);
            } 


            return retorno;
        }


        public Retorno EnviaXMLReducaoZ(string xml, string nomeArquivo)
        {
            Retorno retorno = new Retorno();
            XmlDocument xmlRetorno = new XmlDocument();
            xmlRetorno.LoadXml(client.TransmitirArquivo(Compcatar(xml, nomeArquivo)));
            if(xmlRetorno.SelectSingleNode("Resposta").FirstChild.LocalName == "Recibo")
            {
                retorno.Recibo = xmlRetorno.SelectSingleNode("//Recibo").InnerText;
            }            
            retorno.CodigoProcessamento = int.Parse(xmlRetorno.SelectSingleNode("//SituacaoProcessamentoCodigo").InnerText);
            retorno.Descricao = xmlRetorno.SelectSingleNode("//SituacaoProcessamentoDescricao").InnerText;
            retorno.Mensagem = xmlRetorno.SelectSingleNode("//Mensagem").InnerText;

            return retorno;
        }
        
        public Retorno CancelaReducaoZ(string pXml)
        {
            Retorno retorno = new Retorno();

            XmlDocument xmlRetorno = new XmlDocument();
            string xml = client.CancelarArquivo(pXml);
            xmlRetorno.LoadXml(xml);

            if (int.Parse(xmlRetorno.SelectSingleNode("//SituacaoOperacaoCodigo").InnerText) == 0){
                retorno.Recibo = xmlRetorno.SelectSingleNode("//Recibo").InnerText;
                retorno.CodigoProcessamento = int.Parse(xmlRetorno.SelectSingleNode("//SituacaoProcessamentoCodigo").InnerText);
                retorno.Descricao = xmlRetorno.SelectSingleNode("//SituacaoProcessamentoDescricao").InnerText;
                retorno.Mensagem = xmlRetorno.SelectSingleNode("//SituacaoOperacaoDescricao").InnerText;
            }
            else
            {
                retorno.Recibo = xmlRetorno.SelectSingleNode("//Recibo").InnerText;
                retorno.CodigoProcessamento = int.Parse(xmlRetorno.SelectSingleNode("//SituacaoOperacaoCodigo").InnerText);
                retorno.Mensagem = xmlRetorno.SelectSingleNode("//SituacaoOperacaoDescricao").InnerText;
            }
            

            return retorno;
        }

        public string DownloadXMLReducaoZ(string pXml)
        {
            string dados = string.Empty;
            try
            {
                string xml = client.DownloadArquivo(pXml);

                XmlDocument xmlRetorno = new XmlDocument();
                xmlRetorno.LoadXml(xml);
                if(xmlRetorno.SelectSingleNode("//SituacaoOperacaoDescricao").InnerText != "OK")
                {
                    Console.WriteLine(xmlRetorno.SelectSingleNode("//SituacaoOperacaoDescricao").InnerText);
                }
                else{
                    dados = xmlRetorno.SelectSingleNode("//Arquivo").InnerText;
                }
               
            }
            catch(Exception e)
            {
                Console.WriteLine($"Erro ao tentar fazer o download do arquivo!\n{e.Message}");
                return dados;
            }

            return dados;
        }

        public Byte[] Compcatar(string conteudo, string nomeArquivo)
        {

            byte[] fileBytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                // Cria o zip
                using (ZipArchive zip = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    // Cria o item a ser adicionado dentro do .zip com o nome especificado.
                    ZipArchiveEntry entry = zip.CreateEntry(nomeArquivo + ".xml");

                    using (MemoryStream fileStream = new MemoryStream(Encoding.ASCII.GetBytes(conteudo)))
                    {
                        //Adiciona os Bytes no ZIp
                        using (Stream entryStrem = entry.Open())
                        {
                            fileStream.CopyTo(entryStrem);
                        }
                    }
                }

                fileBytes = stream.ToArray();
                
            }
            return fileBytes;
        }
            
    }
}
