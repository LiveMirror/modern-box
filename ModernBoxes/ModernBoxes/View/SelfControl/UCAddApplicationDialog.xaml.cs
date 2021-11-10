using ModernBoxes.ViewModel;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl
{
    /// <summary>
    /// UCAddApplicationDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UCAddApplicationDialog : UserControl
    {
        public UCAddApplicationDialog()
        {
            InitializeComponent();
            this.DataContext = new UCAddApplicationDialogViewModel();
        }
    }
}