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
    /// UCusedApplications.xaml 的交互逻辑
    /// </summary>
    public partial class UCusedApplications : UserControl
    {
        public UCusedApplications()
        {
            InitializeComponent();
            this.DataContext = new UCusedApplicationViewModel();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //获取右键传递的可执行文件的路径
            Type type = sender.GetType();
            PropertyInfo? propertyInfo = type.GetProperty("CommandParameter");
            String? path = propertyInfo.GetValue(sender).ToString();
            Messenger.Default.Send<String>(path, "path");
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            BaseDialog baseDialog = new BaseDialog();
            String? ApplicationPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (ApplicationPath.Substring(ApplicationPath.LastIndexOf('.') + 1) == "exe" || ApplicationPath.Substring(ApplicationPath.LastIndexOf('.') + 1) == "lnk")
            {
                //如果是超级链接，获取链接的目标地址
                if (ApplicationPath.Substring(ApplicationPath.LastIndexOf('.') + 1) == "lnk" && File.Exists(ApplicationPath))
                {
                    ApplicationPath = GetIcon.getLinkTarget(ApplicationPath);
                }
                baseDialog.SetTitle("添加应用");
                baseDialog.SetHeight(270);
                baseDialog.SetContent(new UCAddApplicationDialog(ApplicationPath));
            }
            else
            {
                baseDialog.SetTitle("提示");
                UcMessageDialog ucMessage = new UcMessageDialog("这里是应用区，请养好分门别类的好习惯", MyEnum.MessageDialogState.Info);
                baseDialog.SetContent(ucMessage);
                baseDialog.SetHeight(170);
            }
            baseDialog.ShowDialog();
        }
    }
}