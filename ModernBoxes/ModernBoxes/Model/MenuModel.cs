using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Model
{
    public class MenuModel
    {


        private String menuName ="";

        public String MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }


        private String icon = "";

        public String Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        private String target = "";

        public String Target
        {
            get { return target; }
            set { target = value; }
        }




    }
}
