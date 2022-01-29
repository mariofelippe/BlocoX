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
                retorno.CodigoProcessamento = int.Parse(xmlRetorno.SelectSingleNode("//SituacaoProcessamentoCodigo").InnerText);
                retorno.Descricao = xmlRetorno.SelectSingleNode("//SituacaoProcessamentoDescricao").InnerText;
                

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
            retorno.Recibo = xmlRetorno.SelectSingleNode("//Recibo").InnerText;
            retorno.CodigoProcessamento = int.Parse(xmlRetorno.SelectSingleNode("//SituacaoProcessamentoCodigo").InnerText);
            retorno.Descricao = xmlRetorno.SelectSingleNode("//SituacaoProcessamentoDescricao").InnerText;
            retorno.Mensagem = xmlRetorno.SelectSingleNode("//Mensagem").InnerText;
           
            

            return retorno;
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
