using ModernBoxes.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Model
{
    public class DirInformationModel
    {
        private String path;

        public String Path
        {
            get { return path; }
            set { path = value; }
        }


        private String kind;

        public String Kind
        {
            get { return kind; }
            set { kind = value; }
        }

        private String size;

        public String Size
        {
            get { return size; }
            set { size = value; }
        }


        private String space;

        public String Space
        {
            get { return space; }
            set { space = value; }
        }

        private String include;

        public String Include
        {
            get { return include; }
            set { include = value; }
        }

        private String  createTime;

        public String  CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private String dirName;

        public String DirName
        {
            get { return dirName; }
            set { dirName = value; }
        }
                

        private DirEnum dirKind;

        public DirEnum DirKind
        {
            get { return dirKind; }
            set { dirKind = value; }
        }



    }
}
