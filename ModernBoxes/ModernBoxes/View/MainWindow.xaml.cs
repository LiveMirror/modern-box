using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.View;
using ModernBoxes.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernBoxes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            this.window.MouseLeftButtonDown += Window_MouseLeftButtonDown;
            this.Height = SystemParameters.PrimaryScreenHeight-70;
            //应用图标不显示在任务栏上
            this.ShowInTaskbar = false;
            //ViewModel注册消息
            Messenger.Default.Register<Boolean>(this, "isShow", ShowCardApplaction);
        }

        /// <summary>
        /// 展示右侧卡片应用
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowCardApplaction(Boolean isShow)
        {
            if (CardApplication.Visibility == Visibility.Visible)
            {
                CardApplication.Visibility = Visibility.Collapsed;
            }
            else
            {
                CardApplication.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_To_CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
        /// <summary>
        /// 重写关闭方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// 控制窗体固定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_To_Fixed(object sender, RoutedEventArgs e)
        {
            if (btn_fixed.IsChecked)
            {
                this.window.MouseLeftButtonDown += Window_MouseLeftButtonDown;
                btn_fixed.IsChecked = false;
            }
            else
            {
                this.window.MouseLeftButtonDown -= Window_MouseLeftButtonDown;
                btn_fixed.IsChecked = true;
            }
        }


        /// <summary>
        /// 窗体拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// 顶层底层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void topItem_Click(object sender, RoutedEventArgs e)
        {
            topItem.IsChecked = true;
            BottomItem.IsChecked = false;
            this.Topmost = true;
        }
        private void BottomItem_Click(object sender, RoutedEventArgs e)
        {
            topItem.IsChecked = false;
            BottomItem.IsChecked = true;
            this.Topmost = false;
        }

       


        //[DllImport("user32.dll")]
        //public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        //public const UInt32 SWP_NOSIZE = 0x0001;
        //public const UInt32 SWP_NOMOVE = 0x0002;
        //public const UInt32 SWP_NOACTIVATE = 0x0010;
        //public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        //private void SetBottom(Window window)
        //{
        //    IntPtr hWnd = new WindowInteropHelper(window).Handle;
        //    SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
        //    SetBottom(this);
        //}


    }
}
