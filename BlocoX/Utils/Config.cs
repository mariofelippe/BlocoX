using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace BlocoX.Utils
{
    public class Config
    {
        private string credenciamento;
        private string listaXML;
        private string pathXML;

        public Config()
        {
            CarregaParametrosConfig();  

        }

        public void CarregaParametrosConfig()
        {          
            credenciamento = ConfigurationManager.AppSettings.Get("CredenciamentoPaf");
            listaXML = ConfigurationManager.AppSettings.Get("ListaXML");
            pathXML = ConfigurationManager.AppSettings.Get("PathXML");
     
        }

        public string Credenciamento { get => credenciamento; }
        public string ListaXML { get => listaXML;  }
        public string PathXML { get => pathXML; }


    }
}
