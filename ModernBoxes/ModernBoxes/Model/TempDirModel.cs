using GalaSoft.MvvmLight;
using ModernBoxes.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Model
{

    public class TempDirModel : ViewModelBase
    {



        private String tempDirPath;

        public String TempDirPath
        {
            get { return tempDirPath; }
            set { tempDirPath = value;RaisePropertyChanged("TempDirPath"); }
        }

        /// <summary>
        /// 红色文件夹 非常重要
        /// 黄色文件夹 重要
        /// 蓝色文件夹 一般
        /// 绿色文件夹 临时(随时可能要删除)
        /// </summary>
        private DirEnum tempDirImportantKind;

        public DirEnum TempDirImportantKind
        {
            get { return tempDirImportantKind; }
            set { tempDirImportantKind = value;RaisePropertyChanged("TempDirImportantKind"); }
        }


    }
}
