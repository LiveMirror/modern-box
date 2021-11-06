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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.ViewModel
{
    public delegate void RefershTTempDirDataHandler();
    public class UCTempDirectoryViewModel : ViewModelBase
    {
        public static event RefershTTempDirDataHandler RefershDataEvent;

        private ObservableCollection<TempDirModel> tempDirs = new ObservableCollection<TempDirModel>();

        public ObservableCollection<TempDirModel> TempDirs
        {
            get { return tempDirs; }
            set { tempDirs = value;RaisePropertyChanged("TempDirs"); }
        }

        /// <summary>
        /// 添加临时文件夹
        /// </summary>
        public RelayCommand AddTempDir
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    BaseDialog baseDialog = new BaseDialog();
                    baseDialog.SetContent(new AddTempDirDialog());
                    baseDialog.SetTitle("添加文件夹");
                    baseDialog.SetHeight(255);
                    baseDialog.ShowDialog();
                }, x => true);
            }
        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        public RelayCommand OpenDir
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    System.Diagnostics.Process.Start("explorer.exe", o.ToString().Replace('/','\\'));
                }, x => true);
            }
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        public RelayCommand NewTempDir
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    BaseDialog dialog = new BaseDialog();
                    dialog.SetTitle("新建文件夹");
                    AddTempDirDialog addTempDirDialog = new AddTempDirDialog();
                    addTempDirDialog.ChangeToNewDirUI();
                    dialog.SetContent(addTempDirDialog);
                    dialog.ShowDialog();
                }, x => true);
            }
        }


        public UCTempDirectoryViewModel()
        {
            Messenger.Default.Register<String>(this, "detempdir", DoDeleteTempDir);
            RefershDataEvent += RefershData;
            init();
        }
        /// <summary>
        /// 删除临时文件夹
        /// </summary>
        /// <param name="path"></param>
        public async void DoDeleteTempDir(String path)
        {
            TempDirModel? tempDirModel = TempDirs.FirstOrDefault(x => x.TempDirPath == path);
            TempDirs.Remove(tempDirModel);
            String json = JsonConvert.SerializeObject(TempDirs);
            File.Delete($"{Environment.CurrentDirectory}\\TempDirConfig.json");
            await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\TempDirConfig.json", json);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefershData()
        {
            TempDirs.Clear();
            init();
        }

        public static void DoRefershData()
        {
            RefershDataEvent();
        }


        /// <summary>
        /// 加载数据
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void init()
        {
            String json = await FileHelper.ReadFile($"{Environment.CurrentDirectory}\\TempDirConfig.json");
            JArray jArray = JArray.Parse(json);
            jArray.Children().ToList().ForEach(x=>TempDirs.Add(x.ToObject<TempDirModel>()));
        }
    }
}
