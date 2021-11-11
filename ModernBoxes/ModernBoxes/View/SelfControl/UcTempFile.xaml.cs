using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl.dialog;
using ModernBoxes.ViewModel;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl
{
    /// <summary>
    /// UcTempFile.xaml 的交互逻辑
    /// </summary>
    public partial class UcTempFile : UserControl
    {
        public UcTempFile()
        {
            InitializeComponent();
            this.DataContext = new UctempFileViewModel();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PropertyInfo? propertyInfo = sender.GetType().GetProperty("CommandParameter");
            String? filePath = propertyInfo.GetValue(sender).ToString();
            Messenger.Default.Send<String>(filePath, "deleteFile");
        }

        /// <summary>
        /// 查看文件属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            PropertyInfo? propertyInfo = sender.GetType().GetProperty("CommandParameter");
            if (propertyInfo != null)
            {
                String? filePath = propertyInfo.GetValue(sender).ToString();
                BaseDialog baseDialog = new BaseDialog();
                baseDialog.SetTitle("文件属性");
                baseDialog.SetHeight(550);
                baseDialog.SetContent(new FilePropertyDialog(filePath));
                baseDialog.ShowDialog();
            }
        }

        /// <summary>
        /// 移除文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveFile_Click(object sender, RoutedEventArgs e)
        {
            String FilePath = sender.GetType().GetProperty("CommandParameter").GetValue(sender).ToString();
            Messenger.Default.Send<String>(FilePath, "RemoveFile");
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            BaseDialog baseDialog = new BaseDialog();
            String FilePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (File.Exists(FilePath))
            {
                //如果是超级链接，获取链接的目标地址
                if (FilePath.Substring(FilePath.LastIndexOf('.') + 1) == "lnk" && File.Exists(FilePath))
                {
                    FilePath = GetIcon.getLinkTarget(FilePath);
                }
                baseDialog.SetTitle("添加文件");
                baseDialog.SetHeight(270);
                baseDialog.SetContent(new AddTempFileDialog(FilePath));
            }
            else
            {
                baseDialog.SetTitle("提示");
                baseDialog.SetHeight(170);
                UcMessageDialog ucMessageDialog = new UcMessageDialog("这是文件区哦，像什么文件之类的还是不要放进来了", MyEnum.MessageDialogState.Info);
                baseDialog.SetContent(ucMessageDialog);
            }

            baseDialog.ShowDialog();
        }
    }
}