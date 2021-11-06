using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Model
{
    public class FileInformationModel
    {
        private String filePath;

        public String FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }


        

        private String createTime;

        public String CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }


        private String changeTime;

        public String ChangeTime
        {
            get { return changeTime; }
            set { changeTime = value; }
        }


        private String size;

        public String Size
        {
            get { return size; }
            set { size = value; }
        }






    }
}
