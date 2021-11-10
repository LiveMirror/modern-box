using HandyControl.Controls;
using ModernBoxes.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

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