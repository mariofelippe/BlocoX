﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace BlocoX.Utils
{
    public class Config
    {
        private string caminhoCertificado;
        private string senhaCertificado;
        private string credenciamento;
        private string listaXML;
        private int tempoEsperaEnvio;
        private string listaConsulta;
        private int tempoEsperaConsulta;
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
            caminhoCertificado = ConfigurationManager.AppSettings.Get("ArquivoCertificado");
            senhaCertificado = ConfigurationManager.AppSettings.Get("SenhaCertificado");
            credenciamento = ConfigurationManager.AppSettings.Get("CredenciamentoPaf");
            listaXML = ConfigurationManager.AppSettings.Get("ListaXML");
            TempoEsperaEnvio = int.Parse(ConfigurationManager.AppSettings.Get("TempoEsperaEnvio"));
            ListaConsulta = ConfigurationManager.AppSettings.Get("ListaConsulta");
            TempoEsperaConsulta = int.Parse(ConfigurationManager.AppSettings.Get("TempoEsperaConsulta"));
            pathXML = ConfigurationManager.AppSettings.Get("PathXML");
            AjustaCredenciamento = bool.Parse(ConfigurationManager.AppSettings.Get("AjustaCredencimanetoPaf"));
            AjustaVendaBrutaDiaria = bool.Parse(ConfigurationManager.AppSettings.Get("AjustaVendaBrutaDiaria"));
            AjustaValorTotalizador = bool.Parse(ConfigurationManager.AppSettings.Get("AjustaValorTotalizador"));           
            
        }

        public string Credenciamento { get => credenciamento; }
        public string ListaXML { get => listaXML;  }
        public string PathXML { get => pathXML; }
        public bool AjustaCredenciamento { get => ajustaCredenciamento; set => ajustaCredenciamento = value; }
        public bool AjustaVendaBrutaDiaria { get => ajustaVendaBrutaDiaria; set => ajustaVendaBrutaDiaria = value; }
        public bool AjustaValorTotalizador { get => ajustaValorTotalizador; set => ajustaValorTotalizador = value; }
        public string CaminhoaCertificado { get => caminhoCertificado; set => caminhoCertificado = value; }
        public string SenhaCertificado { get => senhaCertificado; set => senhaCertificado = value; }
        public string ListaConsulta { get => listaConsulta; set => listaConsulta = value; }
        public int TempoEsperaEnvio { get => tempoEsperaEnvio; set => tempoEsperaEnvio = value; }
        public int TempoEsperaConsulta { get => tempoEsperaConsulta; set => tempoEsperaConsulta = value * 1000; }

        public X509Certificate2 GetCertificado()
        {
            X509Certificate2 certificado = new X509Certificate2(caminhoCertificado, senhaCertificado);
            return certificado;
        }
    }
}
