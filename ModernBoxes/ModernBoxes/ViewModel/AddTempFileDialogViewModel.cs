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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace ModernBoxes.ViewModel
{

    public class AddTempFileDialogViewModel : ViewModelBase
    {

        private List<Boolean> fileKind = new List<bool>() { true, false, false, false };

        public List<Boolean> FileKind
        {
            get { return fileKind; }
            set { fileKind = value; RaisePropertyChanged("FileKind"); }
        }

        private TempFileModel tempFile = new TempFileModel();

        public TempFileModel TempFile
        {
            get { return tempFile; }
            set { tempFile = value; RaisePropertyChanged("TempFile"); }
        }


        private List<TempFileModel> tempFiles = new List<TempFileModel>();

        public AddTempFileDialogViewModel()
        {

        }

        public AddTempFileDialogViewModel(String FileName)
        {
            TempFile.FilePath = FileName;
        }

        public List<TempFileModel> TempFiles
        {
            get { return tempFiles; }
            set { tempFiles = value; }
        }



        /// <summary>
        /// 确定，添加已有文件
        /// </summary>
        public RelayCommand AddTempFile
        {
            get
            {
                return new RelayCommand(async (o) =>
                {
                    if (TempFile.FilePath != String.Empty && TempFile.FilePath != null)
                    {
                        try
                        {
                            ToggleButton? TB_DirRef = o as ToggleButton;
                            //添加模式
                            //获取文件夹类型的信息
                            if (FileKind[0])
                                TempFile.FileKind = MyEnum.DirEnum.dirDanger;
                            if (FileKind[1])
                                TempFile.FileKind = MyEnum.DirEnum.dirWaring;
                            if (FileKind[2])
                                TempFile.FileKind = MyEnum.DirEnum.dirPrimary;
                            if (FileKind[3])
                                TempFile.FileKind = MyEnum.DirEnum.dirSecondary;

                            if (TB_DirRef.Visibility == System.Windows.Visibility.Collapsed)
                            {
                                //新建文件夹模式
                                //获取文件新建文件夹地址
                                TempFile.FilePath = $"{Environment.CurrentDirectory}\\FileCache\\{TempFile.FilePath}";
                                File.Create(TempFile.FilePath);
                            }
                            else
                            {
                                //获取文件夹是否引用
                                if (!(bool)TB_DirRef.IsChecked && TB_DirRef != null)
                                {
                                    //将目标文件夹移动至文件夹缓存区
                                    File.Move(TempFile.FilePath, $"{Environment.CurrentDirectory}\\FileCache\\{TempFile.FilePath.Substring(TempFile.FilePath.LastIndexOf('\\') + 1)}");
                                    //FileHelper.CopyFolder(TempFile.FilePath, $"{Environment.CurrentDirectory}\\FileCache");
                                    TempFile.FilePath = $"{Environment.CurrentDirectory}\\FileCache\\" + TempFile.FilePath.Substring(TempFile.FilePath.LastIndexOf('\\') + 1);
                                }
                            }

                            String oldJson = await FileHelper.ReadFile($"{Environment.CurrentDirectory}\\TempFileConfig.json");
                            if (oldJson.Length > 8)
                            {
                                JArray jArray = JArray.Parse(oldJson);
                                IList<JToken> jTokens = jArray.Children().ToList();
                                foreach (JToken jToken in jTokens)
                                {
                                    if (jToken != null)
                                    {
                                        tempFiles.Add(jToken.ToObject<TempFileModel>());
                                    }
                                }

                            }
                            tempFiles.Add(TempFile);
                            String newJson = JsonConvert.SerializeObject(tempFiles);
                            await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\TempFileConfig.json", newJson);
                            //刷新数据
                            UctempFileViewModel.DoRefershFileData();
                            Messenger.Default.Send<Boolean>(true, "IsCloseBaseDialog");
                        }
                        catch (Exception ex)
                        {
                            //弹窗提示
                            BaseDialog dialog = new BaseDialog();
                            dialog.SetTitle("错误");
                            dialog.SetContent(new UcMessageDialog(ex.Message, MyEnum.MessageDialogState.danger));
                            dialog.SetHeight(180);
                            dialog.ShowDialog();
                        }
                    } 
                }, x => true);
            }
        }


        /// <summary>
        /// 选择文件
        /// </summary>
        public RelayCommand ChoseFile
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                    openFileDialog.Filter = "*|*";
                    openFileDialog.Title = "选择一个文件吧";
                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        TempFile.FilePath = openFileDialog.FileName;
                    }
                }, x => true);
            }
        }
    }
}
