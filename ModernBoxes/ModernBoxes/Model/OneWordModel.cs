using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Model
{
    public class OneWordModel
    {



            public string code { get; set; }
            public string msg { get; set; }
            public Data data { get; set; }


        public class Data
        {
            public string constant { get; set; }
            public string source { get; set; }
        }

    }
}
