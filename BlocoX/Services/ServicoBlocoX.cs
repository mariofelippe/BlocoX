using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlocoX.Utils;

namespace BlocoX.Services
{
    public class ServicoBlocoX
    {
        //private SefazSC.BlocoXSoapClient client = new SefazSC.BlocoXSoapClient();
        private SefazSC.BlocoXSoapClient client = new SefazSC.BlocoXSoapClient();
        private Config config = new Config();
        public string ConsultarProcessamentoArquivo(string recibo)
        {
           
            string pXml = Xml.AssinarXML(Xml.XmlConsultarProcessmentoArquivo(recibo),config.GetCertificado());
            string retorno = client.ConsultarProcessamentoArquivo(pXml);

            return retorno;

        }


            
    }
}
