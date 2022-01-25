using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlocoX.Utils;
using System.Xml;

namespace BlocoX.Services
{
    public class ServicoBlocoX
    {
        //private SefazSC.BlocoXSoapClient client = new SefazSC.BlocoXSoapClient();
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
                
            }


            return retorno;
        }


            
    }
}
