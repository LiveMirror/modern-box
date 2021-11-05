using ModernBoxes.MyEnum;
using ModernBoxes.Tool;
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
    /// UCSetDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UCSetDialog : UserControl
    {
        public UCSetDialog()
        {
            InitializeComponent();
            init();
        }

        /// <summary>
        /// 设置界面初始化
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void init()
        {
            //初始化组件应用的方向/
            CommentLayout layoutOration = (CommentLayout)Enum.Parse(typeof(CommentLayout), ConfigHelper.getConfig("compontentLayout"));
            RB_ShowLeft.IsChecked = layoutOration == CommentLayout.left ? true : false;
            RB_ShowRight.IsChecked = layoutOration == CommentLayout.right ? true : false;
            //设置软件宽高数据初始化
            S_MainWindowHeight.Maximum = SystemParameters.WorkArea.Height;
            S_MainWindowHeight.Value = MainWindow.DoGetMainWindowHeight();

            S_CompontentWidth.Maximum = 420;
            S_CompontentWidth.Value = MainWindow.DoGetCompontentWidth();
        }

        /// <summary>
        /// 切换为光明系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RB_light_Click(object sender, RoutedEventArgs e)
        {
            ChangeTheme(true);
        }

        /// <summary>
        /// 切换为黑暗系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RB_Dark_Click(object sender, RoutedEventArgs e)
        {
            ChangeTheme(false);
        }

        /// <summary>
        /// 切换主题
        /// </summary>
        /// <param name="bol"></param>
        public void ChangeTheme(Boolean bol)
        {
           if (bol)
            {

                Application.Current.Resources.MergedDictionaries.Remove(Application.Current.Resources.MergedDictionaries.FirstOrDefault(o =>
                    o.Source == new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDark.xaml")
                ));
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDefault.xaml")
                });
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Remove(Application.Current.Resources.MergedDictionaries.FirstOrDefault(o =>
                    o.Source == new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDefault.xaml")
                ));
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDark.xaml")
                });
            }
        }

        /// <summary>
        /// 组件应用布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutinleft_Click(object sender, RoutedEventArgs e)
        {
            ConfigHelper.setConfig("compontentLayout", CommentLayout.left);
            //设置完成之后关闭界面中的组件页面
            MainWindow.DoCloseCompontentLayout();
        }

        private void layoutinright_Click(object sender, RoutedEventArgs e)
        {
            ConfigHelper.setConfig("compontentLayout", CommentLayout.right);
            MainWindow.DoCloseCompontentLayout();
        }

        /// <summary>
        /// 修改主界面的高
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void S_MainWindowHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow.DoSetMainWindowHeight(S_MainWindowHeight.Value);
        }

        private void S_CompontentWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow.DoSetCompontentWidth(S_CompontentWidth.Value);
        }
    }
}
