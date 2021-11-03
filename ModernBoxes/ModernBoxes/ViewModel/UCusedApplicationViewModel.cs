using ModernBoxes.Model;
using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.ViewModel
{
    public class UCusedApplicationViewModel
    {
        /// <summary>
        /// 日常应用集合
        /// </summarySystem.InvalidOperationException:“在使用 ItemsSource 之前，项集合必须为空。”

        private ObservableCollection<ApplicationModel> apps = new ObservableCollection<ApplicationModel>();
        public ObservableCollection<ApplicationModel> Apps
        {
            get { return apps; }
            set { apps = value; }
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
        public UCusedApplicationViewModel()
        {
            loadUsedApplication();
        }

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
