using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Model
{
    public class MenuModel :ViewModelBase
    {

        private String menuName ="";

        public String MenuName
        {
            get { return menuName; }
            set { menuName = value; RaisePropertyChanged("MenuName"); }
        }


        private String icon = "";

        public String Icon
        {
            get { return icon; }
            set { icon = value; RaisePropertyChanged("Target"); }
        }

        private String target = "";

        public String Target
        {
            get { return target; }
            set { target = value; RaisePropertyChanged("Target"); }
        }




    }
}
