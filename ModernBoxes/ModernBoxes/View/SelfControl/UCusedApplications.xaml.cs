using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
