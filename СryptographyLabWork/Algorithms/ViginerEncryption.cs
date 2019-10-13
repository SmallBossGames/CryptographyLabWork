using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СryptographyLabWork.Algorithms
{
    static class ViginerEncryption
    {
        public static string Encryption(string source, string key)
        {
            var tempSourceArray = source.ToArray();
            for (int i = 0; i < source.Length; i++)
            {
                tempSourceArray[i] += key[i % key.Length];
            }
            return new string(tempSourceArray);
        }

        public static string Decryption(string source, string key)
        {
            var tempSourceArray = source.ToArray();
            for (int i = 0; i < source.Length; i++)
            {
                tempSourceArray[i] -= key[i % key.Length];
            }
            return new string(tempSourceArray);
        }
    }
}
