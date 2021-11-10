using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl.dialog
{
    /// <summary>
    /// FilePropertyDialog.xaml 的交互逻辑
    /// </summary>
    public partial class FilePropertyDialog : UserControl
    {
        public FilePropertyDialog(String FilePath)
        {
            InitializeComponent();
            this.DataContext = new FilePropertyDialogViewModel(FilePath);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send(true, "IsCloseBaseDialog");
        }
    }
}