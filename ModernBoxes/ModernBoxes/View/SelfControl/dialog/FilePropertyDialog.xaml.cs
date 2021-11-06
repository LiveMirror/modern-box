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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
