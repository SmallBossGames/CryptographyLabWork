using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace СryptographyLabWork.Algorithms
{
    static class TextOffsetCryptography
    {
        public static string Encryption(string source, string key, Encoding encoding)
        {
            var tempSourceByteArray = encoding.GetBytes(source);
            var tempKeyArray = encoding.GetBytes(key);

            for (int i = 0; i < tempSourceByteArray.Length; i++)
            {
                tempSourceByteArray[i] += tempKeyArray[i % tempKeyArray.Length];
            }
            return encoding.GetString(tempSourceByteArray);
        }

        public static string Decryption(string source, string key, Encoding encoding)
        {
            var tempSourceByteArray = encoding.GetBytes(source);
            var tempKeyArray = encoding.GetBytes(key);

            for (int i = 0; i < tempSourceByteArray.Length; i++)
            {
                tempSourceByteArray[i] -= tempKeyArray[i % tempKeyArray.Length];
            }
            return encoding.GetString(tempSourceByteArray);
        }
    }
}