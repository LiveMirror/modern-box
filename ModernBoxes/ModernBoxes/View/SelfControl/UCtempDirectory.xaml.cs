using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.View.SelfControl.dialog;
using ModernBoxes.ViewModel;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl
{
    /// <summary>
    /// UCtempDirectory.xaml 的交互逻辑
    /// </summary>
    public partial class UCtempDirectory : UserControl
    {
        public UCtempDirectory()
        {
            InitializeComponent();
            this.DataContext = new UCTempDirectoryViewModel();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Type type = sender.GetType();
            PropertyInfo? propertyInfo = type.GetProperty("CommandParameter");
            String v = propertyInfo.GetValue(sender).ToString();
            Messenger.Default.Send(v, "detempdir");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Type type = sender.GetType();
            PropertyInfo? propertyInfo = type.GetProperty("CommandParameter");
            String v = propertyInfo.GetValue(sender).ToString();
            BaseDialog baseDialog = new BaseDialog();
            baseDialog.SetTitle("文件夹属性");
            baseDialog.SetHeight(500);
            baseDialog.SetContent(new DirInformationDialog(v.ToString()));
            baseDialog.Show();
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            String? dirPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            BaseDialog baseDialog = new BaseDialog();
            baseDialog.SetTitle("添加文件夹");
            baseDialog.SetHeight(255);
            baseDialog.SetContent(new AddTempDirDialog(dirPath));
            baseDialog.ShowDialog();
        }
    }
}