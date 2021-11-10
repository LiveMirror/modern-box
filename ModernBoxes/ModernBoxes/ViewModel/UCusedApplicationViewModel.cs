using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Model;
using ModernBoxes.Tool;
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
using System.Windows;

namespace ModernBoxes.ViewModel
{
    public delegate void RefershDataHandler();

    public delegate void AddUsedAppHandler(ApplicationModel model);

    public class UCusedApplicationViewModel : ViewModelBase
    {
        public static event RefershDataHandler RefershDataEvent;

        public static event AddUsedAppHandler AddUsedAppEvent;

        /// <summary>
        /// 日常应用集合
        /// </summary>

        private ObservableCollection<ApplicationModel> apps = new ObservableCollection<ApplicationModel>();

        public ObservableCollection<ApplicationModel> Apps
        {
            get
            {
                if (apps.Count > 0)
                    IsShowBgEmpty = Visibility.Collapsed;
                else
                    IsShowBgEmpty = Visibility.Visible;
                return apps;
            }
            set
            {
                apps = value;
                if (apps.Count > 0)
                    IsShowBgEmpty = Visibility.Collapsed;
                else
                    IsShowBgEmpty = Visibility.Visible;
                RaisePropertyChanged("Apps");
            }
        }

        private Visibility isShow = Visibility.Visible;

        public Visibility IsShowBgEmpty
        {
            get { return isShow; }
            set { isShow = value; RaisePropertyChanged("IsShowBgEmpty"); }
        }

        /// <summary>
        /// 打开应用添加对话框
        /// </summary>
        public RelayCommand OpenAddApplicationDialog
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    BaseDialog baseDialog = new BaseDialog();
                    baseDialog.SetTitle("添加应用");
                    baseDialog.SetHeight(270);
                    baseDialog.SetContent(new UCAddApplicationDialog());
                    baseDialog.ShowDialog();
                }, x => true);
            }
        }

        /// <summary>
        /// 管理应用
        /// </summary>
        public RelayCommand ManagerApplication
        {
            get
            {
                return new RelayCommand((o) =>
                {
                }, x => true);
            }
        }

        /// <summary>
        /// 运行程序
        /// </summary>
        public RelayCommand RunApplication
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    try
                    {
                        Process process = new Process();
                        process.StartInfo.FileName = o.ToString();
                        process.Start();
                    }
                    catch (System.ComponentModel.Win32Exception ex)
                    {
                        BaseDialog dialog = new BaseDialog();
                        dialog.SetTitle("错误");
                        dialog.SetContent(new UcMessageDialog("没有找到此应用", MyEnum.MessageDialogState.danger));
                        dialog.SetHeight(180);
                        dialog.ShowDialog();
                    }
                }, x => true);
            }
        }

        public UCusedApplicationViewModel()
        {
            RefershDataEvent += UCusedApplicationViewModel_RefershDataEvent;
            AddUsedAppEvent += UCusedApplicationViewModel_AddUsedAppEvent;
            Messenger.Default.Register<String>(this, "path", toDeleteApplication);
            loadUsedApplication();
        }

        /// <summary>
        /// 添加日常应用
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void UCusedApplicationViewModel_AddUsedAppEvent(ApplicationModel model)
        {
            Apps.Add(model);
        }

        public static void DoAddUsedApp(ApplicationModel model)
        {
            AddUsedAppEvent(model);
        }

        /// <summary>
        /// 删除日常应用
        /// </summary>
        public async void toDeleteApplication(String path)
        {
            ApplicationModel? model = Apps.FirstOrDefault<ApplicationModel>(o => o.AppPath.Contains(path));
            Apps.Remove(model);
            String json = JsonConvert.SerializeObject(Apps);
            //删除原文件写入新文件防止json数据有误
            File.Delete($"{Environment.CurrentDirectory}\\UsedApplicationConfig.json");
            await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\UsedApplicationConfig.json", json);
            //DoRefershData();
        }

        private void UCusedApplicationViewModel_RefershDataEvent()
        {
            Apps.Clear();
            loadUsedApplication();
        }

        public static void DoRefershData()
        {
            RefershDataEvent();
        }

        /// <summary>
        /// 加载应用
        /// </summary>
        public async void loadUsedApplication()
        {
            String json = await FileHelper.ReadFile($"{Environment.CurrentDirectory}\\UsedApplicationConfig.json");
            JArray jArray = JArray.Parse(json);
            IList<JToken> templist = jArray.Children().ToList();
            foreach (JToken jToken in templist)
            {
                if (jToken != null)
                {
                    Apps.Add(jToken.ToObject<ApplicationModel>());
                    //第一次添加应用后将SVG空状态图变为Collapsed
                    IsShowBgEmpty = Visibility.Collapsed;
                }
            }
        }
    }
}