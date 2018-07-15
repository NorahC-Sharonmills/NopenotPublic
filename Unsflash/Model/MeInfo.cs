using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsflash.Model
{
    public class MeInfo
    {
        public class Links
        {
            public string self { get; set; }
            public string html { get; set; }
            public string photos { get; set; }
            public string likes { get; set; }
            public string portfolio { get; set; }
            public string following { get; set; }
            public string followers { get; set; }
        }

        public class ProfileImage
        {
            public string small { get; set; }
            public string medium { get; set; }
            public string large { get; set; }
        }

        public class Custom
        {
            public string title { get; set; }
        }

        public class Aggregated
        {
            public string title { get; set; }
        }

        public class Tags
        {
            public List<Custom> custom { get; set; }
            public List<Aggregated> aggregated { get; set; }
        }
    }
}
