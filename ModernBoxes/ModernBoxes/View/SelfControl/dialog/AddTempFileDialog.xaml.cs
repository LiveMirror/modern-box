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
    /// AddTempFileDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddTempFileDialog : UserControl
    {
        public AddTempFileDialog()
        {
            InitializeComponent();
            this.DataContext = new AddTempFileDialogViewModel();
        }


        public AddTempFileDialog(String FileName)
        {
            InitializeComponent();
            this.DataContext = new AddTempFileDialogViewModel(FileName);
        }

        public void ChangeToNewDirUI()
        {
            SP_isRef.Visibility = Visibility.Collapsed;
            TB_DirRef.Visibility = Visibility.Collapsed;
            InfoElement.SetPlaceholder(TB_DirPath, "文件名");
            btn_ChooseDirPath.Visibility = Visibility.Collapsed;
        }
    }
}
