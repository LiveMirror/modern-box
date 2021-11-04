using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.ViewModel
{
    public delegate void RefershDataHandler();
    public class UCusedApplicationViewModel : ViewModelBase
    {
        public static event RefershDataHandler RefershDataEvent;
        /// <summary>
        /// 日常应用集合
        /// </summarySystem.InvalidOperationException:“在使用 ItemsSource 之前，项集合必须为空。”

        private ObservableCollection<ApplicationModel> apps = new ObservableCollection<ApplicationModel>();
        public ObservableCollection<ApplicationModel> Apps
        {
            get { return apps; }
            set { apps = value; RaisePropertyChanged("Apps"); }
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
                    Process process = new Process();
                    process.StartInfo.FileName = o.ToString();
                    process.Start();
                }, x => true);
            }
        }


        public UCusedApplicationViewModel()
        {
            RefershDataEvent += UCusedApplicationViewModel_RefershDataEvent;
            Messenger.Default.Register<String>(this, "path", toDeleteApplication);
            loadUsedApplication();
        }

        /// <summary>
        /// 删除日常应用
        /// </summary>
        public async void toDeleteApplication(String path)
        {
            ApplicationModel? model =Apps.FirstOrDefault<ApplicationModel>(o => o.AppPath.Contains(path));
            Apps.Remove(model);
            String json = JsonConvert.SerializeObject(Apps);
            //删除原文件写入新文件防止json数据有误
            File.Delete($"{Environment.CurrentDirectory}\\UsedApplicationConfig.json");
            await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\UsedApplicationConfig.json", json);
            DoRefershData();
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
                if(jToken != null)
                    Apps.Add(jToken.ToObject<ApplicationModel>());
            }
        }
    }
}
