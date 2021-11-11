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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace ModernBoxes.ViewModel
{
    public delegate void RefershFileDataHandler();

    public delegate void AddFileItemHandler(TempFileModel model);

    public class UctempFileViewModel : ViewModelBase
    {
        public static event RefershFileDataHandler RefershFileDataEvent;

        public static event AddFileItemHandler AddFileItemEvent;

        private TempFileModel tempFile = new TempFileModel();

        public TempFileModel TempFile
        {
            get { return tempFile; }
            set { tempFile = value; RaisePropertyChanged("TempFile"); }
        }

        private ObservableCollection<TempFileModel> tempFiles = new ObservableCollection<TempFileModel>();

        public ObservableCollection<TempFileModel> TempFiles
        {
            get
            {
                if (tempFiles.Count > 0)
                {
                    BgEmptyShow = Visibility.Collapsed;
                }
                else
                {
                    BgEmptyShow = Visibility.Visible;
                }
                return tempFiles;
            }
            set { tempFiles = value; RaisePropertyChanged("TempFiles"); }
        }

        private Visibility bgEmptyShow;

        public Visibility BgEmptyShow
        {
            get { return bgEmptyShow; }
            set { bgEmptyShow = value; RaisePropertyChanged("BgEmptyShow"); }
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
                    AddTempFileDialog addTempFileDialog = new AddTempFileDialog();
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
                    try
                    {
                        ProcessStartInfo processStartInfo = new ProcessStartInfo(o.ToString());
                        Process process = new Process();
                        process.StartInfo = processStartInfo;
                        process.StartInfo.UseShellExecute = true;
                        process.Start();
                    }
                    catch (Exception ex)
                    {
                        BaseDialog dialog = new BaseDialog();
                        dialog.SetTitle("错误");
                        dialog.SetContent(new UcMessageDialog("没有找到此文件", MyEnum.MessageDialogState.danger));
                        dialog.SetHeight(200);
                        dialog.ShowDialog();
                    }
                }, x => true);
            }
        }

        public UctempFileViewModel()
        {
            RefershFileDataEvent += UctempFileViewModel_RefershFileDataEvent;
            AddFileItemEvent += UctempFileViewModel_AddFileItemEvent;
            Messenger.Default.Register<String>(this, "deleteFile", DoDeleteFile);
            Messenger.Default.Register<String>(this, "RemoveFile", RemoveFile);
            init();
        }

        /// <summary>
        /// 移除文件，在文件卡片中删除但是不删除源文件
        /// </summary>
        /// <param name="FilePath"></param>
        public async void RemoveFile(String FilePath)
        {
            if (File.Exists(FilePath))
            {
                TempFiles.Remove(TempFiles.FirstOrDefault(o => o.FilePath == FilePath));
                String newJson = JsonConvert.SerializeObject(TempFiles);
                File.Delete($"{Environment.CurrentDirectory}\\TempFileConfig.json");
                await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\TempFileConfig.json", newJson);
            }
        }

        /// <summary>
        /// 添加临时文件
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void UctempFileViewModel_AddFileItemEvent(TempFileModel model)
        {
            TempFiles.Add(model);
        }

        public static void DoAddFileItem(TempFileModel model)
        {
            AddFileItemEvent(model);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FilePath"></param>
        public async void DoDeleteFile(String FilePath)
        {
            try
            {
                TempFileModel? tempFileModel = TempFiles.FirstOrDefault(o => o.FilePath == FilePath);
                if (tempFileModel != null)
                {
                    TempFiles.Remove(tempFileModel);
                    File.Delete(FilePath);
                    String json = JsonConvert.SerializeObject(TempFiles);
                    File.Delete($"{Environment.CurrentDirectory}\\TempFileConfig.json");
                    await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\TempFileConfig.json", json);
                }
            }
            catch (Exception ex)
            {
                BaseDialog baseDialog = new BaseDialog();
                UcMessageDialog ucMessageDialog = new UcMessageDialog(ex.Message, MyEnum.MessageDialogState.waring);
                baseDialog.SetTitle("警告");
                baseDialog.SetContent(ucMessageDialog);
                baseDialog.ShowDialog();
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
            if (json.Length > 8)
            {
                JArray jArray = JArray.Parse(json);
                jArray.Children().ToList().ForEach(x => TempFiles.Add(x.ToObject<TempFileModel>()));
                if (jArray.Count > 0)
                {
                    BgEmptyShow = Visibility.Collapsed;
                }
            }
        }
    }
}