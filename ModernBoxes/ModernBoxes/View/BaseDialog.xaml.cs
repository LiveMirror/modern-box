using GalaSoft.MvvmLight.Messaging;
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
using System.Windows.Shapes;

namespace ModernBoxes.View.SelfControl
{
    /// <summary>
    /// BaseDialog.xaml 的交互逻辑
    /// </summary>
    public partial class BaseDialog : Window
    {

        public BaseDialog()
        {
            InitializeComponent();
            Messenger.Default.Register<Boolean>(this, "IsCloseBaseDialog", closeBaseDialog);
        }

        public void closeBaseDialog(Boolean bol)
        {
            if (bol)
            {
                this.Close();
            }
        }

        public BaseDialog(String title,Object content)
        {
            this.TB_DialogTitle.Text = title;
            this.Content = Content;
        }


        /// <summary>
        /// 设置对黄框标题
        /// </summary>
        /// <param name="title"></param>
        public void SetTitle(String title)
        {
            this.TB_DialogTitle.Text = title;
        }

        /// <summary>
        /// 设置对话框内容
        /// </summary>
        /// <param name="content"></param>
        public void SetContent(Object content)
        {
            if (content!=null)
            {
                this.DialogContent.Content = content;
            }
        }

        /// <summary>
        /// 设置对话框宽高
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void setDialogSize(int width,int height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// 设置对话框的宽
        /// </summary>
        /// <param name="width"></param>
        public void SetWidth(int width)
        {
            this.Width = width;
        }

        /// <summary>
        /// 设置对话框的高
        /// </summary>
        /// <param name="height"></param>
        public void SetHeight(int height)
        {
            this.Height= height;
        }


        /// <summary>
        /// 关闭对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
