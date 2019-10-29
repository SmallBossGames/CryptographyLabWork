using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СryptographyLabWork.Algorithms
{
    public sealed class CustomEncoding
    {
        readonly Dictionary<char, uint> directDict;
        readonly Dictionary<uint, char> reverseDict;
        readonly ushort count;

        static CustomEncoding m_singleton;

        CustomEncoding()
        {
            (directDict, reverseDict, count) = BuildEncodingTable();
        }

        public static CustomEncoding Singleton 
        {
            get
            {
                if (m_singleton == null)
                    m_singleton = new CustomEncoding();

                return m_singleton;
            }
        }

        public ushort Count => count;

        public uint ToUInt16(char chartacter) 
            => directDict[chartacter];

        public char ToChar(uint code)
            => reverseDict[code];

        private static (Dictionary<char, uint>, Dictionary<uint, char>, ushort count) BuildEncodingTable()
        {
            var directDict = new Dictionary<char, uint>(256);
            var reverseDict = new Dictionary<uint, char>(256);

            ushort j = 0;

            for (char i = ' '; i <= '@'; i++, j++)
            {
                directDict[i] = j;
                reverseDict[j] = i;
            }

            for (char i = 'a'; i <= 'z'; i++, j++)
            {
                directDict[i] = j;
                reverseDict[j] = i;
            }

            for (char i = 'A'; i <= 'Z'; i++, j++)
            {
                directDict[i] = j;
                reverseDict[j] = i;
            }

            for (char i = 'а'; i <= 'я'; i++, j++)
            {
                directDict[i] = j;
                reverseDict[j] = i;
            }

            for (char i = 'А'; i <= 'Я'; i++, j++)
            {
                directDict[i] = j;
                reverseDict[j] = i;
            }

            var unincludedChars = new char[] { 'ё', 'Ё', '~', '\n', '\r' };
            for (int i = 0; i < unincludedChars.Length; i++, j++)
            {
                directDict.Add(unincludedChars[i], j);
                reverseDict.Add(j, unincludedChars[i]);
            }

            return (directDict, reverseDict, j);
        }
    }
}
