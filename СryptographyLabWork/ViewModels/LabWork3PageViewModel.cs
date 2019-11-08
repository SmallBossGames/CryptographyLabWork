using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using СryptographyLabWork.Algorithms;

namespace СryptographyLabWork.ViewModels
{
    class LabWork3PageViewModel: ViewModelBase
    {
        private StorageFile storageFile;
        private ulong calculatedCrc;
        private ulong loadedCrcValue;

        public LabWork3PageViewModel()
        {
            ProcessingAlgorythms = new List<Tuple<string, ProcessingAlgorythm>>
            {
                new Tuple<string, ProcessingAlgorythm>("CRC-8", ProcessingAlgorythm.CRC8),
                new Tuple<string, ProcessingAlgorythm>("CRC-16", ProcessingAlgorythm.CRC16),
                new Tuple<string, ProcessingAlgorythm>("CRC-32", ProcessingAlgorythm.CRC32),
                new Tuple<string, ProcessingAlgorythm>("CRC-64", ProcessingAlgorythm.CRC64),
            };
            storageFile = null;
            ProcessingAlgorythmItem = ProcessingAlgorythms[0];
        }

        public List<Tuple<string, ProcessingAlgorythm>> ProcessingAlgorythms { get; }

        public string StorageFileName
            => storageFile == null ? string.Empty :  storageFile.Path;

        public Tuple<string, ProcessingAlgorythm> ProcessingAlgorythmItem { get; set; }

        public StorageFile StorageFile
        {
            get => storageFile;
            set
            {
                SetMember(ref storageFile, value);
                OnPropertyChanged(nameof(StorageFileName));
            }
        }

        public ulong CalculatedCrc 
        { 
            get => calculatedCrc; 
            set => SetMember(ref calculatedCrc, value); 
        }

        public ulong LoadedCrcValue 
        { 
            get => loadedCrcValue; 
            set => SetMember(ref loadedCrcValue, value); 
        }

        public bool CRCCheckResult => CalculatedCrc == LoadedCrcValue;

        public async Task AppendCrcMarkAsync()
        {
            if (storageFile == null)
                return;
            
            byte[] fileBytes = Array.Empty<byte>();

            using (var stream = await StorageFile.OpenStreamForReadAsync())
            {
                fileBytes = new byte[stream.Length];
                await stream.ReadAsync(fileBytes, 0, fileBytes.Length);
            }

            byte[] crcBytes = Array.Empty<byte>();

            switch (ProcessingAlgorythmItem.Item2)
            {
                case ProcessingAlgorythm.CRC8:
                    {
                        var result = CalculationCRC.CalculationCRC8(fileBytes);
                        crcBytes = new[] { result };
                    }
                    break;
                case ProcessingAlgorythm.CRC16:
                    {
                        var result = CalculationCRC.CalculationCRC16(fileBytes);
                        crcBytes = BitConverter.GetBytes(result);
                    }
                    break;
                case ProcessingAlgorythm.CRC32:
                    {
                        var result = CalculationCRC.CalculationCRC32(fileBytes);
                        crcBytes = BitConverter.GetBytes(result);
                    }
                    break;
                case ProcessingAlgorythm.CRC64:
                    {
                        var result = CalculationCRC.CalculationCRC64(fileBytes);
                        crcBytes = BitConverter.GetBytes(result);
                    }
                    break;
                default:
                    break;
            }

            CachedFileManager.DeferUpdates(StorageFile);
            
            using (var stream = await StorageFile.OpenStreamForWriteAsync())
            {
                stream.Seek(0, SeekOrigin.End);
                await stream.WriteAsync(crcBytes, 0, crcBytes.Length);
            }

            _ = await CachedFileManager.CompleteUpdatesAsync(StorageFile);
        }

        public async Task CheckCRCMarkAync(CoreDispatcher dispatcher)
        {
            if (storageFile == null)
                return;

            byte[] fileBytes = Array.Empty<byte>();
            byte[] crcBytes = Array.Empty<byte>();

            int crcSize;

            switch (ProcessingAlgorythmItem.Item2)
            {
                case ProcessingAlgorythm.CRC8:
                    crcSize = sizeof(byte);
                    break;
                case ProcessingAlgorythm.CRC16:
                    crcSize = sizeof(ushort);
                    break;
                case ProcessingAlgorythm.CRC32:
                    crcSize = sizeof(uint);
                    break;
                case ProcessingAlgorythm.CRC64:
                    crcSize = sizeof(ulong);
                    break;
                default:
                    crcSize = 0;
                    break;
            }
            
            using (var stream = await StorageFile.OpenStreamForReadAsync())
            {
                if (stream.Length < crcSize) return;
                
                var dataSize = stream.Length - crcSize;
                
                fileBytes = new byte[dataSize];
                crcBytes = new byte[crcSize];

                await stream.ReadAsync(fileBytes, 0, fileBytes.Length);
                await stream.ReadAsync(crcBytes, 0, crcBytes.Length);
            }

            ulong crcCalculatedValue;
            ulong crcFromFileValue;

            switch (ProcessingAlgorythmItem.Item2)
            {
                case ProcessingAlgorythm.CRC8:
                    crcFromFileValue = crcBytes[0];
                    crcCalculatedValue = CalculationCRC.CalculationCRC8(fileBytes);
                    break;
                case ProcessingAlgorythm.CRC16:
                    crcFromFileValue = BitConverter.ToUInt16(crcBytes, 0);
                    crcCalculatedValue = CalculationCRC.CalculationCRC16(fileBytes);
                    break;
                case ProcessingAlgorythm.CRC32:
                    crcFromFileValue = BitConverter.ToUInt32(crcBytes, 0);
                    crcCalculatedValue = CalculationCRC.CalculationCRC32(fileBytes);
                    break;
                case ProcessingAlgorythm.CRC64:
                    crcFromFileValue = BitConverter.ToUInt64(crcBytes, 0);
                    crcCalculatedValue = CalculationCRC.CalculationCRC64(fileBytes);
                    break;
                default:
                    crcFromFileValue = crcCalculatedValue = 0;
                    break;
            }

            await dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                LoadedCrcValue = crcFromFileValue;
                CalculatedCrc = crcCalculatedValue;
                OnPropertyChanged(nameof(CRCCheckResult));
            });
        }

        public enum ProcessingAlgorythm
        {
            CRC8, CRC16, CRC32, CRC64
        }
    }
}
