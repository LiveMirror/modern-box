using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Model
{
    public class ApplicationModel
    {
        private String appPath;

        public String AppPath
        {
            get { return appPath; }
            set { appPath = value; }
        }

        private String icon;

        public String Icon
        {
            get { return icon; }
            set { icon = value; }
        }


    }
}
