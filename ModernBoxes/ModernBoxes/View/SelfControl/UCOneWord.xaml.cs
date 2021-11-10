using ModernBoxes.ViewModel;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl
{
    /// <summary>
    /// UCOneWord.xaml 的交互逻辑
    /// </summary>
    public partial class UCOneWord : UserControl
    {
        public UCOneWord()
        {
            InitializeComponent();
            this.DataContext = new OneWordViewModel();
        }
    }
}