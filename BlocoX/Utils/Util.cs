using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;

namespace BlocoX.Utils
{
    public class Util
    {

        public static DateTime StrToDate(string strData)
        {
            DateTime data = new DateTime();

            data = DateTime.Parse(strData);
            return data;
            
        }

        public static int StrIntToClear(string valor)
        {
                     
            try
            {
                Regex regex = new Regex(@"\D+");
                valor = regex.Replace(valor, @"");
                return int.Parse(valor);
            }
            catch(FormatException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            
        }

        public static decimal StrDecimalToClear(string valor)
        {
            try
            {
                Regex regex = new Regex(@"\D+");
                valor = regex.Replace(valor, @"");
                return decimal.Parse(valor);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            
        }


        public static string RemoverPontoVirgula(string str)
        {
            return str.Replace(".", "").Replace(",", "");
        }

        public static string AdiconaZeroEsqueda(int quantidade, string valor)
        {
            return valor.PadLeft(quantidade, '0');
        }

        public static void SalvaLogRetorno(string nomeArquivo, string conteudo)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(nomeArquivo));
            using (StreamWriter arquivo = File.AppendText(nomeArquivo))
            {
                arquivo.WriteLine(conteudo);
            }

        }

        public static void CompatactarXML(string caminhoArquivo)
        {
            string caminho = Path.GetFullPath(caminhoArquivo).Replace(Path.GetFileName(caminhoArquivo), "");
            string nome = Path.GetFileNameWithoutExtension(caminhoArquivo);
            
            using(FileStream stream = new FileStream(caminho + nome + ".zip", FileMode.Create))
            {
                using(ZipArchive zip = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry entry = zip.CreateEntry(nome + ".xml");
                    using(Stream streamEntry = entry.Open())
                    {
                        using(FileStream streamXML = new FileStream(caminhoArquivo, FileMode.Open))
                        {
                            streamXML.CopyTo(streamEntry);
                        }
                        
                    }
                }
            }
        }

        public static void SalvarArquivoBase64(string conteudo)
        {

            if (!Directory.Exists("Downloads"))
            {
                Directory.CreateDirectory("Downloads");
            }

            if (string.IsNullOrEmpty(conteudo))
            {
                return;
            }

            Byte[] dados = Convert.FromBase64String(conteudo);
            using (MemoryStream st = new MemoryStream(dados))
            {
                ZipArchive zip = new ZipArchive(st, ZipArchiveMode.Read);
                foreach(ZipArchiveEntry entry in zip.Entries)
                {
                    string nome = entry.Name.Split('.')[0];
                    File.WriteAllBytes("Downloads\\" + nome + ".zip", dados);
                   
                }
            }
        }
    }
}
