using GalaSoft.MvvmLight;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernBoxes.ViewModel
{
    public class DirInformationDialogViewModel : ViewModelBase
    {
        private DirInformationModel dirInfo = new DirInformationModel();

        public DirInformationModel DirInfo
        {
            get { return dirInfo; }
            set { dirInfo = value; }
        }

        public DirInformationDialogViewModel(String path)
        {
            DirInfo.Path = path;
            init();
        }

        private async void init()
        {
            DirInfo.CreateTime = Directory.GetCreationTime(DirInfo.Path).ToShortDateString();
            DirInfo.DirName = DirInfo.Path.Substring(DirInfo.Path.LastIndexOf('\\') + 1);
            DirInfo.Include = "文件数" + Directory.GetFiles(DirInfo.Path).Length.ToString() + "文件夹数" + Directory.GetDirectories(DirInfo.Path).Length.ToString();

            String json = await FileHelper.ReadFile($"{Environment.CurrentDirectory}\\TempDirConfig.json");
            JArray jArray = JArray.Parse(json);
            IList<JToken> jTokens = jArray.Children().ToList();
            JToken? jToken = jTokens.FirstOrDefault(o => o.ToObject<TempDirModel>().TempDirPath == DirInfo.Path);
            DirInfo.DirKind = jToken.ToObject<TempDirModel>().TempDirImportantKind;
        }
    }
}