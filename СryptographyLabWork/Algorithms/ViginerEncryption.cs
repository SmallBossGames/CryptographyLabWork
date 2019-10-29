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
            var enc = CustomEncoding.Singleton;
            var resultChars = new char[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                var sourceCode = enc.ToUInt16(source[i]);
                var keyCode = enc.ToUInt16(key[i % key.Length]);
                var code = GetDirectExchangingCode(sourceCode, keyCode, enc);
                resultChars[i] = enc.ToChar(code);
            }

            return new string(resultChars);
        }

        public static string Decryption(string source, string key)
        {
            var enc = CustomEncoding.Singleton;
            var resultChars = new char[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                var sourceCode = enc.ToUInt16(source[i]);
                var keyCode = enc.ToUInt16(key[i % key.Length]);
                var code = GetReverseExchangingCode(sourceCode, keyCode, enc);
                resultChars[i] = enc.ToChar(code);
            }

            return new string(resultChars);
        }

        private static uint GetReverseExchangingCode(uint charCode, uint keyCharCode, CustomEncoding encoding)
            => charCode < keyCharCode ? encoding.Count + charCode - keyCharCode : charCode - keyCharCode;

        private static uint GetDirectExchangingCode(uint charCode, uint keyCharCode, CustomEncoding encoding)
            => charCode >= encoding.Count - keyCharCode ? charCode - encoding.Count + keyCharCode : charCode + keyCharCode;
    }
}
