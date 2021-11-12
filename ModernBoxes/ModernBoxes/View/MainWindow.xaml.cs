using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Model;
using ModernBoxes.MyEnum;
using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl;
using ModernBoxes.View.SelfControl.dialog;
using ModernBoxes.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ModernBoxes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public delegate void CloseCompontentLayoutHandler();

    /// <summary>
    /// 获取或设置主窗体的宽高
    /// </summary>
    /// <returns></returns>
    public delegate Double getMainWindowHeightHandler();

    public delegate void SetMainWidnowHeightHandler(Double value);

    /// <summary>
    /// 获取或设置卡片的宽高
    /// </summary>
    /// <returns></returns>
    public delegate Double getCompontentWidthHandler();

    public delegate void SetCompontentWidthHandler(Double value);

    public partial class MainWindow : Window
    {
        public static event CloseCompontentLayoutHandler CloseCompontentLayoutEvent;

        public static event getMainWindowHeightHandler GetMainWindowHeightEvent;

        public static event SetMainWidnowHeightHandler SetMainWindowHeightEvent;

        public static event getCompontentWidthHandler GetCompontentWidthEvent;

        public static event SetCompontentWidthHandler SetCompontentWidthEvent;

        public List<CardContentModel> CardApps = new List<CardContentModel>();

        /// <summary>
        /// 布局方向
        /// </summary>
        private CommentLayout commentLayout = CommentLayout.right;

        public MainWindow()
        {
            InitializeComponent();
            initConfig();
            CloseCompontentLayoutEvent += MainWindow_CloseCompontentLayoutEvent;
            GetMainWindowHeightEvent += MainWindow_GetMainWindowHeightEvent;
            SetMainWindowHeightEvent += MainWindow_SetMainWindowHeightEvent;
            GetCompontentWidthEvent += MainWindow_GetCompontentWidthEvent;
            SetCompontentWidthEvent += MainWindow_SetCompontentWidthEvent;

            if (ConfigHelper.getConfig("x") != String.Empty)
            {
                //按照上一次固定的位置显示程序
                this.WindowStartupLocation = WindowStartupLocation.Manual;
                this.Left = Convert.ToInt32(ConfigHelper.getConfig("x"));
                this.Top = Convert.ToInt32(ConfigHelper.getConfig("y"));
            }
            this.DataContext = new MainViewModel();
            this.window.MouseLeftButtonDown += Window_MouseLeftButtonDown;
            this.Height = SystemParameters.PrimaryScreenHeight - 70;
            //应用图标不显示在任务栏上
            this.ShowInTaskbar = false;
            //ViewModel注册消息
            Messenger.Default.Register<Boolean>(this, "isShow", ShowCardApplaction);

            loadComment();
        }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        private async void initConfig()
        {
            String config = await FileHelper.ReadFile($"{Environment.CurrentDirectory}\\AllCardsConfig.json");
            //当配置文件被误删之后重新生成卡片配置文件
            if ((ConfigHelper.getConfig("isFirst")) == String.Empty||config.Length<8)
            {
                ConfigHelper.setConfig("isFirst", "true");
                //默认展开的地方在右侧
                ConfigHelper.setConfig("compontentLayout", CommentLayout.right);
                //首次使用创建缓存文件夹
                Directory.CreateDirectory($"{Environment.CurrentDirectory}\\DirCache");
                Directory.CreateDirectory($"{Environment.CurrentDirectory}\\FileCache");
                //默认设置不自启动
                ConfigHelper.setConfig("autoOpen", false);
                //卡片配置文件生成
                CardApps.Add(new CardContentModel() { CardName = "一言", IsChecked = true, CardID = 0, CardHeight = 100, Priview = "/Resource/image/previews/onenote1.png" });
                CardApps.Add(new CardContentModel() { CardName = "应用", IsChecked = true, CardID = 1, CardHeight = 235, Priview = "/Resource/image/previews/application.png" });
                CardApps.Add(new CardContentModel() { CardName = "文件夹", IsChecked = true, CardID = 2, CardHeight = 235, Priview = "/Resource/image/previews/dir1.png" });
                CardApps.Add(new CardContentModel() { CardName = "文件", IsChecked = true, CardID = 3, CardHeight = 235, Priview = "/Resource/image/previews/file1.png" });
                CardApps.Add(new CardContentModel() { CardName = "便签", IsChecked = false, CardID = 4, CardHeight = 235, Priview = "/Resource/image/previews/notes1.png" });
                string CardJson = JsonConvert.SerializeObject(CardApps);
                await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\AllCardsConfig.json", CardJson);
            }
        }

        /// <summary>
        /// 加载组件应用
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void loadComment()
        {
            commentLayout = (CommentLayout)Enum.Parse(typeof(CommentLayout), ConfigHelper.getConfig("compontentLayout"));
            if (commentLayout == CommentLayout.left)
            {
                compontentLayoutLeft.Content = new UcCompotent();
            }
            else
            {
                compontentLayoutRight.Content = new UcCompotent();
            }
        }

        /// <summary>
        /// 展示右侧卡片应用
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowCardApplaction(Boolean isShow)
        {
            if (commentLayout == CommentLayout.right)
            {
                compontentLayoutRight.Visibility = (compontentLayoutRight.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible);
            }
            else
            {
                compontentLayoutLeft.Visibility = (compontentLayoutLeft.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible);
            }
        }

        private void MainWindow_CloseCompontentLayoutEvent()
        {
            CloseCompontentLayout();
        }

        public static void DoCloseCompontentLayout()
        {
            CloseCompontentLayoutEvent();
        }

        /// <summary>
        /// 在设置中更换布局位置后隐藏组件布局
        /// </summary>
        public void CloseCompontentLayout()
        {
            compontentLayoutLeft.Visibility = Visibility.Hidden;
            compontentLayoutRight.Visibility = Visibility.Hidden;
            //根据配置文件中的布局参数重新加载
            loadComment();
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
                //固定后记录程序的位置
                ConfigHelper.setConfig("x", this.Left);
                ConfigHelper.setConfig("y", this.Top);
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

        /// <summary>
        /// 设置主界面的高
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private void MainWindow_SetMainWindowHeightEvent(double value)
        {
            this.Height = value;
        }

        public static void DoSetMainWindowHeight(Double value)
        {
            SetMainWindowHeightEvent(value);
        }

        /// <summary>
        /// 获取主界面的高
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private double MainWindow_GetMainWindowHeightEvent()
        {
            return this.Height;
        }

        public static Double DoGetMainWindowHeight()
        {
            return GetMainWindowHeightEvent();
        }

        /// <summary>
        /// 获取或设置组件应用的宽
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void MainWindow_SetCompontentWidthEvent(double value)
        {
            compontentLayoutLeft.Width = value;
            compontentLayoutRight.Width = value;
        }

        public static void DoSetCompontentWidth(Double value)
        {
            SetCompontentWidthEvent(value);
        }

        private double MainWindow_GetCompontentWidthEvent()
        {
            return compontentLayoutLeft.Width;
        }

        public static Double DoGetCompontentWidth()
        {
            return GetCompontentWidthEvent();
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
        /// 打开添加卡片应用的对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCardApp_Click(object sender, RoutedEventArgs e)
        {
            BaseDialog baseDialog = new BaseDialog();
            baseDialog.SetTitle("添加卡片应用");
            baseDialog.setDialogSize(565, 400);
            baseDialog.SetContent(new UcAddCardApplicationDialog());
            baseDialog.ShowDialog();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            String menuName = sender.GetType().GetProperty("CommandParameter").GetValue(sender).ToString();
            MainViewModel.DoDeleteMenuItem(menuName);
            MainViewModel.DoRefershMenu();
        }
    }
}