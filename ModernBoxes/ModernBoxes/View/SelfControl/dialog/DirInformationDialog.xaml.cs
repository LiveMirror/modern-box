using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl.dialog
{
    /// <summary>
    /// DirInformationDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DirInformationDialog : UserControl
    {
        public DirInformationDialog(String path)
        {
            InitializeComponent();
            this.DataContext = new DirInformationDialogViewModel(path);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<Boolean>(true, "IsCloseBaseDialog");
        }
    }
}