using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
    }
}
