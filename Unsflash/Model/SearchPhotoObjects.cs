using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Unsflash.Model.SearchPhoto;

namespace Unsflash.Model
{
    public class SearchPhotoObjects
    {
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Result> results { get; set; }
    }
}
