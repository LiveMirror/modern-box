using ModernBoxes.ViewModel;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl
{
    /// <summary>
    /// UcCompotent.xaml 的交互逻辑
    /// </summary>
    public partial class UcCompotent : UserControl
    {
        public UcCompotent()
        {
            InitializeComponent();
            this.DataContext = new UcCompontentViewModel();
        }
    }
}