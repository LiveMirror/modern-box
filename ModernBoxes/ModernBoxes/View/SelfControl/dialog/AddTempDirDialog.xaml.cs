using HandyControl.Controls;
using ModernBoxes.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

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