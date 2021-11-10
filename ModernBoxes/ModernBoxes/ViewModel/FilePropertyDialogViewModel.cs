using GalaSoft.MvvmLight;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using System;
using System.IO;

namespace ModernBoxes.ViewModel
{
    public class FilePropertyDialogViewModel : ViewModelBase
    {
        private FileInformationModel fileInformation = new FileInformationModel();

        public FileInformationModel FileInformation
        {
            get { return fileInformation; }
            set { fileInformation = value; RaisePropertyChanged("FileInformation"); }
        }

        public FilePropertyDialogViewModel(String FilePath)
        {
            FileInformation.FilePath = FilePath;
            FileInformation.CreateTime = File.GetCreationTime(FilePath).ToString();
            FileInformation.ChangeTime = File.GetLastWriteTime(FilePath).ToString() == String.Empty ? "暂时没有修改此文件" : File.GetLastWriteTime(FilePath).ToString();
            FileInformation.Size = FileHelper.getFileSize(FilePath).ToString() + " Byte";
        }
    }
}