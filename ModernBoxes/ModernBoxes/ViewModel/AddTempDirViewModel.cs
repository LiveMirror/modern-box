using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.ViewModel
{
    public class AddTempDirViewModel : ViewModelBase
    {
        /// <summary>
        /// 单选按钮默认选择第一个
        /// </summary>
        private List<Boolean> dirKind = new List<bool>() { true,false,false,false};

        public List<Boolean> DirKind
        {
            get { return dirKind; }
            set { dirKind = value; RaisePropertyChanged("DirKind"); }
        }

        private TempDirModel dirmodel = new TempDirModel();

        public TempDirModel DirModel
        {
            get { return dirmodel; }
            set { dirmodel = value; RaisePropertyChanged("DirModel"); }
        }


        private List<TempDirModel> tempDirs = new List<TempDirModel>();

        public List<TempDirModel> TempDirs
        {
            get { return tempDirs; }
            set { tempDirs = value; }
        }


        /// <summary>
        /// 添加文件夹
        /// </summary>
        public RelayCommand AddTempDir {
            get
            {
                return new RelayCommand(async(o) =>
                {
                    if (DirModel.TempDirPath != String.Empty && DirModel.TempDirPath != null)
                    {
                        if (dirKind[0])
                            DirModel.TempDirImportantKind = MyEnum.DirEnum.dirDanger;
                        if (dirKind[1])
                            DirModel.TempDirImportantKind = MyEnum.DirEnum.dirWaring;
                        if (dirKind[2])
                            DirModel.TempDirImportantKind = MyEnum.DirEnum.dirPrimary;
                        if (dirKind[3])
                            DirModel.TempDirImportantKind = MyEnum.DirEnum.dirSecondary;

                        String oldJson = await FileHelper.ReadFile($"{Environment.CurrentDirectory}\\TempDirConfig.json");
                        if (oldJson.Length > 8)
                        {
                            JArray jArray = JArray.Parse(oldJson);
                            IList<JToken> jTokens = jArray.Children().ToList();
                            foreach (JToken jToken in jTokens)
                            {
                                if (jToken!=null)
                                {
                                    TempDirs.Add(jToken.ToObject<TempDirModel>());
                                }
                            }

                        }
                        TempDirs.Add(DirModel);
                        String newJson = JsonConvert.SerializeObject(TempDirs);
                        await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\TempDirConfig.json", newJson);
                        UCTempDirectoryViewModel.DoRefershData();
                        Messenger.Default.Send<Boolean>(true, "IsCloseBaseDialog");
                    }
                }, x => true);
            }
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        public RelayCommand ChoseDir
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                    if (dialog.ShowDialog()== System.Windows.Forms.DialogResult.OK)
                    {
                        DirModel.TempDirPath = dialog.SelectedPath;
                    }
                }, x => true);
            }
        }

       

      

        public AddTempDirViewModel()
        {

        }
    }
}
