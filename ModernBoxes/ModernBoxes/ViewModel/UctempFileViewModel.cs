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
    public delegate void RefershFileDataHandler();
    public class UctempFileViewModel : ViewModelBase
    {
        public static event RefershFileDataHandler RefershFileDataEvent;

        private TempFileModel tempFile = new TempFileModel();

        public TempFileModel TempFile
        {
            get { return tempFile; }
            set { tempFile = value;RaisePropertyChanged("TempFile"); }
        }


        private ObservableCollection<TempFileModel> tempFiles = new ObservableCollection<TempFileModel>();

        public ObservableCollection<TempFileModel> TempFiles
        {
            get { return tempFiles; }
            set { tempFiles = value;RaisePropertyChanged("TempFiles"); }
        }



        /// <summary>
        /// 新建文件
        /// </summary>
        public RelayCommand NewTempFile
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    BaseDialog baseDialog = new BaseDialog();
                    baseDialog.SetTitle("新建文件");
                    AddTempFileDialog addTempFileDialog= new AddTempFileDialog();
                    addTempFileDialog.ChangeToNewDirUI();
                    baseDialog.SetHeight(200);
                    baseDialog.SetContent(addTempFileDialog);
                    baseDialog.ShowDialog();
                }, x => true);
            }
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        public RelayCommand AddTempFile
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    BaseDialog baseDialog = new BaseDialog();
                    baseDialog.SetTitle("选择文件");
                    baseDialog.SetContent(new AddTempFileDialog());
                    baseDialog.ShowDialog();
                }, x => true);
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        public RelayCommand OpenFile
        {
            get
            {
                return new RelayCommand((o) =>
                {

                }, x => true);
            }
        }

        public UctempFileViewModel()
        {
            RefershFileDataEvent += UctempFileViewModel_RefershFileDataEvent;
            Messenger.Default.Register<String>(this, "deleteFile", DoDeleteFile);
            init();
        }

        public async void DoDeleteFile(String FilePath)
        {
            TempFileModel? tempFileModel = TempFiles.FirstOrDefault(o => o.FilePath == FilePath);
            if (tempFileModel != null)
            {
                TempFiles.Remove(tempFileModel);
                File.Delete(FilePath);
                String json = JsonConvert.SerializeObject(TempFiles);
                File.Delete($"{Environment.CurrentDirectory}\\TempDirConfig.json");
                await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\TempFileConfig.json", json);
            }
        }


        private void UctempFileViewModel_RefershFileDataEvent()
        {
            TempFiles.Clear();
            init();
        }

        public static void DoRefershFileData()
        {
            RefershFileDataEvent();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void init()
        {
            String json = await FileHelper.ReadFile($"{Environment.CurrentDirectory}\\TempFileConfig.json");
            JArray jArray = JArray.Parse(json);
            jArray.Children().ToList().ForEach(x => TempFiles.Add(x.ToObject<TempFileModel>()));
        }
    }
}
