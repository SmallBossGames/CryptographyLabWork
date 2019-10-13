using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СryptographyLabWork.Algorithms;

namespace СryptographyLabWork.ViewModels
{
    public class LabWork1PageViewModel : ViewModelBase
    {
        private string encodingKey;
        private string sourceText;
        private string encodedText;
        private KeyValuePair<string, ProcessingMode> processingModeItem;

        public LabWork1PageViewModel()
        {
            ProcessingModes = new List<KeyValuePair<string, ProcessingMode>>(2)
            {
                KeyValuePair.Create("Кодирование", ProcessingMode.Encoding),
                KeyValuePair.Create("Декодирование", ProcessingMode.Decoding),
            };

            ProcessingModeItem = ProcessingModes[0];

            EncodingKey = SourceText = ProcessedText = string.Empty;
        }

        public List<KeyValuePair<string, ProcessingMode>> ProcessingModes { get; }

        public KeyValuePair<string, ProcessingMode> ProcessingModeItem
        {
            get => processingModeItem;
            set
            {
                processingModeItem = value;
                OnPropertyChanged(nameof(ProcessingMode));
            }
        }

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

        public void ProcessSource()
        {
            if (string.IsNullOrEmpty(EncodingKey))
            {
                ProcessedText = SourceText;
                return;
            }

            switch (ProcessingModeItem.Value)
            {
                case ProcessingMode.Encoding:
                    ProcessedText = TextOffsetCryptography.Encryption(SourceText, EncodingKey, Encoding.Unicode);
                    break;
                case ProcessingMode.Decoding:
                    ProcessedText = TextOffsetCryptography.Decryption(SourceText, EncodingKey, Encoding.Unicode);
                    break;
                default:
                    break;
            }
        }
    }
}
