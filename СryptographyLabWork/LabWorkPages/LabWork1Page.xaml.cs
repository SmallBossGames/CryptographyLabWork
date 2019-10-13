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
using СryptographyLabWork.ViewModels;

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
    }
}
