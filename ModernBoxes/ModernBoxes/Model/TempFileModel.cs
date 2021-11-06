using GalaSoft.MvvmLight;
using ModernBoxes.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Model
{
    public class TempFileModel : ViewModelBase
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        private String filePath;

        public String FilePath
        {
            get { return filePath; }
            set { filePath = value;RaisePropertyChanged("FilePath"); }
        }



        /// <summary>
        /// 文件类型
        /// </summary>
        private DirEnum  fileKind;

        public DirEnum FileKind
        {
            get { return fileKind; }
            set { fileKind = value; RaisePropertyChanged("FileKind"); }
        }


    }
}
