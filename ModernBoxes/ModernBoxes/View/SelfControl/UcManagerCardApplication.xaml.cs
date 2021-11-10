using ModernBoxes.ViewModel;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl
{
    /// <summary>
    /// UcManagerCardApplication.xaml 的交互逻辑
    /// </summary>
    public partial class UcManagerCardApplication : UserControl
    {
        public UcManagerCardApplication()
        {
            InitializeComponent();
            this.DataContext = new UcManngerCardAppViewModel();
        }
    }
}