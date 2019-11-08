using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using СryptographyLabWork.Algorithms;

namespace СryptographyLabWork.ViewModels
{
    class LabWork3PageViewModel: ViewModelBase
    {
        private StorageFile storageFile;

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
                        var result = CalculationCRC.CalculationCRC16(fileBytes);
                        crcBytes = BitConverter.GetBytes(result);
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

        public enum ProcessingAlgorythm
        {
            CRC8, CRC16, CRC32, CRC64
        }
    }
}
