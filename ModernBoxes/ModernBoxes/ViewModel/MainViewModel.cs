using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using ModernBoxes.View;
using ModernBoxes.View.SelfControl;
using ModernBoxes.View.SelfControl.dialog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ModernBoxes.ViewModel
{
    public delegate void RefreshMenu();

    public delegate void DeleteMenuItemHandler(String menuName);

    public class MainViewModel : ViewModelBase
    {
        public static event DeleteMenuItemHandler DeleteMenuItemEvent;

        public static event RefreshMenu RefreshMenuEvent;

        /// <summary>
        /// 主菜单集合
        /// </summary>
        private ObservableCollection<MenuModel> menus = new ObservableCollection<MenuModel>();

        public ObservableCollection<MenuModel> MenuList
        {
            get { return menus; }
            set { menus = value; RaisePropertyChanged("MenuList"); }
        }

        /// <summary>
        /// 点击菜单加载命令
        /// </summary>
        public RelayCommand OpenApp
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    if (File.Exists(o.ToString()))
                    {
                        //打开文件
                        ProcessStartInfo processStartInfo = new ProcessStartInfo(o.ToString());
                        Process process = new Process();
                        process.StartInfo = processStartInfo;
                        process.StartInfo.UseShellExecute = true;
                        process.Start();
                    }
                    else if (Directory.Exists(o.ToString()))
                    {
                        System.Diagnostics.Process.Start("explorer.exe", o.ToString().Replace('/', '\\'));
                    }
                    else if (o.ToString().Equals("组件应用"))
                    {
                        Messenger.Default.Send<Boolean>(true, "isShow");
                    }
                }, x => true);
            }
        }

        /// <summary>
        /// 打开添加主菜单的对话框
        /// </summary>
        public RelayCommand AddMenuDialog
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    AddMenuDialog addmenuDialog = new AddMenuDialog();
                    addmenuDialog.ShowDialog();
                }, x => true);
            }
        }

        /// <summary>
        /// 打开设置对话框
        /// </summary>
        public RelayCommand OpenSetDialog
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    BaseDialog dialog = new BaseDialog();
                    dialog.SetTitle("设置");
                    dialog.setDialogSize(560, 800);
                    dialog.SetContent(new UCSetDialog());
                    dialog.ShowDialog();
                }, x => true);
            }
        }

        public MainViewModel()
        {
            RefreshMenuEvent += MainViewModel_RefreshMenuEvent;
            DeleteMenuItemEvent += MainViewModel_DeleteMenuItemEvent;
            loadMenu();
        }

        /// <summary>
        /// 删除菜单项
        /// </summary>
        /// <param name="menuName"></param>
        private async void MainViewModel_DeleteMenuItemEvent(string menuName)
        {
            //当删除组件应用菜单后关闭卡片
            if(menuName == "组件应用")
            {
                MainWindow.DoCloseCompontentLayout();
            }
            MenuList.Remove(MenuList.FirstOrDefault(o => o.MenuName == menuName));
            if (File.Exists($"{Environment.CurrentDirectory}\\MenuConfig.json"))
            {
                File.Delete($"{Environment.CurrentDirectory}\\MenuConfig.json");
            }
            String newJson = JsonConvert.SerializeObject(MenuList);
            await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\MenuConfig.json", newJson);
        }

        public static void DoDeleteMenuItem(String menuName)
        {
            DeleteMenuItemEvent(menuName);
        }

        /// <summary>
        /// 添加菜单后刷新界面
        /// </summary>
        /// <param name="bol"></param>
        private void MainViewModel_RefreshMenuEvent()
        {
            MenuList.Clear();
            loadMenu();
        }

        public static void DoRefershMenu()
        {
            RefreshMenuEvent();
        }

        /// <summary>
        /// 加载主菜单项
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void loadMenu()
        {
            String json = await FileHelper.ReadFile($"{Environment.CurrentDirectory}\\MenuConfig.json");
            if (json.Length > 8)
            {
                JArray array = JArray.Parse(json);
                IList<JToken> temp = array.Children().ToList();
                foreach (JToken tempItem in temp)
                {
                    if (tempItem != null)
                        MenuList.Add(tempItem.ToObject<MenuModel>());
                }
            }
        }
    }
}