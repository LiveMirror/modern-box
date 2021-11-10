using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.ViewModel;
using System;
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
    }
}