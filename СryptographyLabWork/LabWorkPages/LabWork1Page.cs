using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;
using СryptographyLabWork.ViewModels;
using System.Threading.Tasks;
using System.Threading;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace СryptographyLabWork.LabWorkPages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LabWork1Page : Page
    {
        public LabWork1Page()
        {
            ViewModel = new LabWork1PageViewModel();
            this.InitializeComponent();
        }

        private LabWork1PageViewModel ViewModel { get; set; }

        private void SourceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.ProcessSource();
        }

        private void EncodingKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.ProcessSource();
        }

        private void ProcessingModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ProcessSource();
        }

        private void EncryptionAlgorithmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ProcessSource();
        }

        private async void LoadSourceButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".txt");

            var file = await picker.PickSingleFileAsync();

            if(file!=null)
            {
                var text = await FileIO.ReadTextAsync(file);
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                {
                    ViewModel.SourceText = text;
                });
            }
        }

        private async void SaveSourceButton_Click(object sender, RoutedEventArgs e)
        {
            await SaveData(SourceTextBox.Text);
        }

        private async void SaveProducedButton_Click(object sender, RoutedEventArgs e)
        {
            await SaveData(ProcessedTextBox.Text);
        }


        private async Task SaveData(string text)
        {
            var picker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "Новый документ"
            };
            picker.FileTypeChoices.Add("Текст", new List<string>() { ".txt" });

            var file = await picker.PickSaveFileAsync();

            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, text);
                _ = await CachedFileManager.CompleteUpdatesAsync(file);
            }
        }
    }
}
