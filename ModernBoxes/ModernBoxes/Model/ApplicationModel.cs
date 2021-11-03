using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Model
{
    public class ApplicationModel : ViewModelBase
    {

        private String fileName;

        public String FileName
        {
            get { return fileName; }
            set { fileName = value;RaisePropertyChanged("FileName"); }
        }


        private String appPath;
        public String AppPath
        {
            get { return appPath; }
            set { appPath = value;RaisePropertyChanged("AppPath"); }
        }


        private String icon;

        public String Icon
        {
            get { return icon; }
            set { icon = value; RaisePropertyChanged("Icon"); }
        }


    }
}
