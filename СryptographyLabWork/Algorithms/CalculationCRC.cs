using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СryptographyLabWork.Algorithms
{
    static class CalculationCRC
    {
        public static ulong CalculationCRC64(byte[] sourceBytes, ulong CRCBase)
        {
            NormalizeArray(ref sourceBytes, sizeof(ulong));
            
            var value = BitConverter.ToUInt64(sourceBytes, 0);

            for (int i = 0; i < sourceBytes.Length - sizeof(ulong); i++)
            {
                var hotLoadIndex = sizeof(ulong) + i;
                var hotLoadByte = (ulong)sourceBytes[hotLoadIndex];

                for (int j = sizeof(byte) - 1; j >= 0 ; j--)
                {
                    value = (value << 1) | (hotLoadByte >> j) & 1UL;
                    if ((value & 1UL<<63) != 0)
                    {
                        value ^= CRCBase;
                    }
                }
            }
            return value;
        }

        private static void NormalizeArray(ref byte[] source, int minCount)
        {
            if (source.Length >= minCount)
                return;
            Array.Resize(ref source, minCount);
        }
    }
}
