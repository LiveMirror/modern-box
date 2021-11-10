using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace ModernBoxes.View
{
    /// <summary>
    /// AddMenuDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddMenuDialog : Window
    {
        public AddMenuDialog()
        {
            InitializeComponent();
            this.DataContext = new AddMenuDialogViewModel();
            Messenger.Default.Register<Boolean>(this, "IsCloseDialog", CloseDialog);
        }

        public void CloseDialog(Boolean bol)
        {
            if (bol)
                this.Close();
        }

        private void Window_MouseLeftButtonDown_To_Drave(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}