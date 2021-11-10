using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl;
using ModernBoxes.View.SelfControl.dialog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace ModernBoxes.ViewModel
{
    public delegate void RefershTTempDirDataHandler();

    public delegate void AddTempDirHandler(TempDirModel model);

    public class UCTempDirectoryViewModel : ViewModelBase
    {
        public static event RefershTTempDirDataHandler RefershDataEvent;

        public static event AddTempDirHandler AddTempDirEvent;

        private ObservableCollection<TempDirModel> tempDirs = new ObservableCollection<TempDirModel>();

        public ObservableCollection<TempDirModel> TempDirs
        {
            get
            {
                if (tempDirs.Count > 0)
                {
                    BgEmptyShow = Visibility.Collapsed;
                }
                else
                {
                    BgEmptyShow = Visibility.Visible;
                }
                return tempDirs;
            }
            set { tempDirs = value; RaisePropertyChanged("TempDirs"); }
        }

        private Visibility bgEmptyShow = Visibility.Visible;

        public Visibility BgEmptyShow
        {
            get { return bgEmptyShow; }
            set { bgEmptyShow = value; RaisePropertyChanged("BgEmptyShow"); }
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
                    System.Diagnostics.Process.Start("explorer.exe", o.ToString().Replace('/', '\\'));
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
            AddTempDirEvent += UCTempDirectoryViewModel_AddTempDirEvent;
            init();
        }

        /// <summary>
        /// 添加临时文件夹
        /// </summary>
        /// <param name="model"></param>
        private void UCTempDirectoryViewModel_AddTempDirEvent(TempDirModel model)
        {
            TempDirs.Add(model);
        }

        public static void DoAddTempDirItem(TempDirModel model)
        {
            AddTempDirEvent(model);
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
            jArray.Children().ToList().ForEach(x => TempDirs.Add(x.ToObject<TempDirModel>()));
            if (jArray.Children().ToList().Count > 0)
            {
                BgEmptyShow = Visibility.Collapsed;
            }
        }
    }
}