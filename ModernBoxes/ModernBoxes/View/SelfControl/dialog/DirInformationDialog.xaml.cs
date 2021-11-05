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
