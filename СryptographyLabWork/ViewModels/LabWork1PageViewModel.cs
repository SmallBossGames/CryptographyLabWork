using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СryptographyLabWork.Algorithms;
using System.Net.NetworkInformation;

namespace СryptographyLabWork.ViewModels
{
    public class LabWork1PageViewModel : ViewModelBase
    {
        private string encodingKey;
        private string sourceText;
        private string encodedText;

        public LabWork1PageViewModel()
        {
            EncryptionAlgorithms = new List<Tuple<string, EncryptionAlgorithm>>(2)
            {
                Tuple.Create("Гаммированием", EncryptionAlgorithm.Offset),
                Tuple.Create("Заменой", EncryptionAlgorithm.ViginerSwap),
            };

            ProcessingModes = new List<Tuple<string, ProcessingMode>>(2)
            {
                Tuple.Create("Кодирование", ProcessingMode.Encoding),
                Tuple.Create("Декодирование", ProcessingMode.Decoding),
            };

            ProcessingModeItem = ProcessingModes[0];
            EncryptionAlgorithmItem = EncryptionAlgorithms[0];

            EncodingKey = SourceText = ProcessedText = string.Empty;
        }

        public List<Tuple<string, ProcessingMode>> ProcessingModes { get; }
        public List<Tuple<string, EncryptionAlgorithm>> EncryptionAlgorithms { get; }

        public string EncodingKey
        {
            get => encodingKey;
            set
            {
                encodingKey = value;
                OnPropertyChanged(nameof(EncodingKey));
            }
        }

        public string SourceText
        {
            get => sourceText;
            set
            {
                sourceText = value;
                OnPropertyChanged(nameof(SourceText));
            }
        }

        public string ProcessedText
        {
            get => encodedText;
            set
            {
                encodedText = value;
                OnPropertyChanged(nameof(ProcessedText));
            }
        }

        public Tuple<string, ProcessingMode> ProcessingModeItem { get; set; }

        public Tuple<string, EncryptionAlgorithm> EncryptionAlgorithmItem { get; set; }

        public void ProcessSource()
        {
            if (!CheckDRM())
            {
                throw new Exception();
            }


            if (string.IsNullOrEmpty(EncodingKey))
            {
                ProcessedText = SourceText;
                return;
            }

            switch (ProcessingModeItem.Item2)
            {
                case ProcessingMode.Encoding:
                    switch (EncryptionAlgorithmItem.Item2)
                    {
                        case EncryptionAlgorithm.Offset:
                            ProcessedText = TextOffsetCryptography.Encryption(SourceText, EncodingKey, Encoding.Unicode);
                            break;
                        case EncryptionAlgorithm.ViginerSwap:
                            ProcessedText = ViginerEncryption.Encryption(SourceText, EncodingKey);
                            break;
                        default:
                            break;
                    }
                    break;
                case ProcessingMode.Decoding:
                    switch (EncryptionAlgorithmItem.Item2)
                    {
                        case EncryptionAlgorithm.Offset:
                            ProcessedText = TextOffsetCryptography.Decryption(SourceText, EncodingKey, Encoding.Unicode);
                            break;
                        case EncryptionAlgorithm.ViginerSwap:
                            ProcessedText = ViginerEncryption.Decryption(SourceText, EncodingKey);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        public static bool CheckDRM()
        {
            var targetPhysicalAddress = "E006E652EBC9";
            var result = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var item in result)
            {
                var mac = item.GetPhysicalAddress().ToString();
                if (targetPhysicalAddress == mac)
                    return true;
            }
            return false;
        }
    }
}
