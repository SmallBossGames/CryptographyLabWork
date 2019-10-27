using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СryptographyLabWork.Algorithms
{
    public sealed class CustomEncoding
    {
        readonly Dictionary<char, ushort> directDict;
        readonly Dictionary<ushort, char> reverseDict;
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

        public ushort ToUInt16(char chartacter) 
            => directDict[chartacter];

        public char ToChar(ushort code)
            => reverseDict[code];

        private static (Dictionary<char, ushort>, Dictionary<ushort, char>, ushort count) BuildEncodingTable()
        {
            var directDict = new Dictionary<char, ushort>(256);
            var reverseDict = new Dictionary<ushort, char>(256);

            ushort j = 0;

            for (char i = '!'; i <= '@'; i++, j++)
            {
                directDict[i] = j;
                reverseDict[j] = i;
            }

            for (char i = 'a'; i < 'z'; i++, j++)
            {
                directDict[i] = j;
                reverseDict[j] = i;
            }

            for (char i = 'A'; i < 'Z'; i++, j++)
            {
                directDict[i] = j;
                reverseDict[j] = i;
            }

            for (char i = 'а'; i < 'я'; i++, j++)
            {
                directDict[i] = j;
                reverseDict[j] = i;
            }

            for (char i = 'А'; i < 'Я'; i++, j++)
            {
                directDict[i] = j;
                reverseDict[j] = i;
            }

            return (directDict, reverseDict, j);
        }
    }
}
