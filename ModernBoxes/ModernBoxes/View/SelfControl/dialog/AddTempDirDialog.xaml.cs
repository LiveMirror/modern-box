using HandyControl.Controls;
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
    /// AddTempDirDialog.xaml 的交互逻辑
    /// </summary>
    /// 



    public partial class AddTempDirDialog : UserControl
    {
        

        public AddTempDirDialog()
        {
            InitializeComponent();
            this.DataContext = new AddTempDirViewModel();
        }

        public AddTempDirDialog(String DirPath)
        {
            InitializeComponent();
            this.DataContext = new AddTempDirViewModel(DirPath);
        }

        public void ChangeToNewDirUI()
        {
            SP_isRef.Visibility = Visibility.Collapsed;
            TB_DirRef.Visibility = Visibility.Collapsed;
            InfoElement.SetPlaceholder(TB_DirPath, "文件夹名称");
            btn_ChooseDirPath.Visibility = Visibility.Collapsed;
        }

    }
}
