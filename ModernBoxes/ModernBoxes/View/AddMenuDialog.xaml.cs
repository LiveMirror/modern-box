using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
