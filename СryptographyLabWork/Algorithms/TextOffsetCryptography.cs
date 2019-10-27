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
            var enc = CustomEncoding.Singleton;
            var resultChars = new char[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                var sourceCode = enc.ToUInt16(source[i]);
                var keyCode = enc.ToUInt16(key[i % key.Length]);

                var code = (ushort)((sourceCode + keyCode) % enc.Count);
                resultChars[i] = enc.ToChar(code);
            }

            return new string(resultChars);
        }

        public static string Decryption(string source, string key, Encoding encoding)
        {
            var enc = CustomEncoding.Singleton;
            var resultChars = new char[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                var sourceCode = enc.ToUInt16(source[i]);
                var keyCode = enc.ToUInt16(key[i % key.Length]);
                
                if (keyCode > sourceCode)
                    sourceCode += enc.Count;

                var code = (ushort)(sourceCode - keyCode);
                
                resultChars[i] = enc.ToChar(code);
            }

            return new string(resultChars);
        }
    }
}