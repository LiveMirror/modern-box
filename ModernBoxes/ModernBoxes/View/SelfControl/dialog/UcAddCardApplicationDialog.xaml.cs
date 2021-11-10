using ModernBoxes.ViewModel;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl.dialog
{
    /// <summary>
    /// UcAddCardApplicationDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UcAddCardApplicationDialog : UserControl
    {
        public UcAddCardApplicationDialog()
        {
            InitializeComponent();
            this.DataContext = new UcAddCardAppDialogViewModel();
        }
    }
}