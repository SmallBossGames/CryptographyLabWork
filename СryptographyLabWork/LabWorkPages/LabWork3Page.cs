using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using СryptographyLabWork.ViewModels;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace СryptographyLabWork.LabWorkPages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LabWork3Page : Page
    {
        public LabWork3Page()
        {
            ViewModel = new LabWork3PageViewModel();
            this.InitializeComponent();
        }

        private LabWork3PageViewModel ViewModel { get; set; }

        private async void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".txt");

            var file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                {
                    ViewModel.StorageFile = file;
                });
            }
        }

        private async void AddCrcMarkButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.AppendCrcMarkAsync();
        }
    }
}
