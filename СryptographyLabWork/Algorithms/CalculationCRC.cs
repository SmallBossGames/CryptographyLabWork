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
        public static ulong CalculationCRC64(byte[] sourceBytes)
        {
            var crcTable = BuildReverseCRC64Table(0xC96C5795D7870F42);

            var crc = 0xffffffffffffffffUL;

            foreach (var item in sourceBytes)
            {
                crc = crcTable[(crc ^ item) & 0xFFUL] ^ (crc >> 8);
            }
            return crc ^ 0xffffffffffffffffUL;
        }

        public static uint CalculationCRC32(byte[] sourceBytes)
        {
            var crcTable = BuildReverseCRC32Table(0xEDB88320U);

            var crc = 0xffffffffU;

            foreach (var item in sourceBytes)
            {
                crc = crcTable[(crc ^ item) & 0xFF] ^ (crc >> 8);
            }
            return crc ^ 0xFFFFFFFFU;
        }

        public static ushort CalculationCRC16(byte[] sourceBytes)
        {
            var crcTable = BuildReverseCRC16Table(0xA001);

            ushort crc = 0xffff;

            foreach (var item in sourceBytes)
            {
                crc = (ushort)(crcTable[(crc ^ item) & 0xFF] ^ (crc >> 8));
            }
            return (ushort)(crc ^ 0xffff);
        }

        public static byte CalculationCRC8(byte[] sourceBytes)
        {
            byte crc = 0xFF;

            for (int i = 0; i < sourceBytes.Length; i++)
            {
                crc ^= sourceBytes[i];

                for (int j = 0; j < 8; j++)
                {
                    crc = (byte)((crc & 0x80) != 0 ? (crc << 1) ^ 0x31 : crc << 1);
                }
            }

            return crc;
        }

        private static uint[] BuildReverseCRC32Table(uint crcRevBase)
        {
            var crcTable = new uint[256];

            for (uint i = 0; i < crcTable.Length; i++)
            {
                var tempCrc = i;
                for (int j = 0; j < 8; j++)
                {
                    tempCrc = (tempCrc & 1U) != 0 ? (tempCrc >> 1) ^ crcRevBase : tempCrc >> 1;
                }

                crcTable[i] = tempCrc;
            }

            return crcTable;
        }

        private static ulong[] BuildReverseCRC64Table(ulong crcRevBase)
        {
            var crcTable = new ulong[256];

            for (int i = 0; i < crcTable.Length; i++)
            {
                var tempCrc = (ulong)i;
                for (int j = 0; j < 8; j++)
                {
                    tempCrc = (tempCrc & 1U) != 0 ? (tempCrc >> 1) ^ crcRevBase : tempCrc >> 1;
                }

                crcTable[i] = tempCrc;
            }

            return crcTable;
        }

        private static ushort[] BuildReverseCRC16Table(ushort crcRevBase)
        {
            var crcTable = new ushort[256];

            for (ushort i = 0; i < crcTable.Length; i++)
            {
                var tempCrc = i;
                for (int j = 0; j < 8; j++)
                {
                    tempCrc = (ushort)((tempCrc & 1) != 0 ? (tempCrc >> 1) ^ crcRevBase : tempCrc >> 1);
                }
                crcTable[i] = tempCrc;
            }

            return crcTable;
        }
    }
}
