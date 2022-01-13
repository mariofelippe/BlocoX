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
        private bool ajustaCredenciamento;
        private bool ajustaVendaBrutaDiaria;
        private bool ajustaValorTotalizador;
        public Config()
        {
            CarregaParametrosConfig();  

        }

        public void CarregaParametrosConfig()
        {
            ConfigurationManager.RefreshSection("appSettings");
            credenciamento = ConfigurationManager.AppSettings.Get("CredenciamentoPaf");
            listaXML = ConfigurationManager.AppSettings.Get("ListaXML");
            pathXML = ConfigurationManager.AppSettings.Get("PathXML");
            AjustaCredenciamento = bool.Parse(ConfigurationManager.AppSettings.Get("AjustaCredencimanetoPaf"));
            AjustaVendaBrutaDiaria = bool.Parse(ConfigurationManager.AppSettings.Get("AjustaVendaBrutaDiaria"));
            AjustaValorTotalizador = bool.Parse(ConfigurationManager.AppSettings.Get("AjustaValorTotalizaro"));
            


        }

        public string Credenciamento { get => credenciamento; }
        public string ListaXML { get => listaXML;  }
        public string PathXML { get => pathXML; }
        public bool AjustaCredenciamento { get => ajustaCredenciamento; set => ajustaCredenciamento = value; }
        public bool AjustaVendaBrutaDiaria { get => ajustaVendaBrutaDiaria; set => ajustaVendaBrutaDiaria = value; }
        public bool AjustaValorTotalizador { get => ajustaValorTotalizador; set => ajustaValorTotalizador = value; }
    }
}
