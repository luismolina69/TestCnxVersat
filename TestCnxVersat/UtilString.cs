using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCnxVersat
{
    class UtilString
    {

        /// <summary>
        /// Inserta un caracter cada n posiciones en un string
        /// </summary>
        /// <param name="aStr"> string a insertar los caracter</param>
        /// <param name="achar"> caracter a insertar</param>
        /// <param name="longValue">posicion donde se inserta el caracter</param>
        /// <returns></returns>
        public static string InsertnChar(string aStr, string achar, int longValue)
        {
            string[] arr = SplitStr(aStr, longValue);
            string result = string.Join(achar.ToString(), arr);
            return result;
        }

        /// <summary>
        /// divide una cadena en partes de longitud especifica y las retorna en un array
        /// </summary>
        /// <param name="input">Caneda a dividir</param>
        /// <param name="length">Longitud</param>
        /// <returns></returns>
        private static string[] SplitStr(string input, int length)
        {
            if (string.IsNullOrEmpty(input))
                return new string[0];

            int numChunks = (int)Math.Ceiling((double)input.Length / length);
            return Enumerable.Range(0, numChunks)
                             .Select(i => input.Substring(i * length, Math.Min(length, input.Length - i * length)))
                             .ToArray();
        }
    }

}
