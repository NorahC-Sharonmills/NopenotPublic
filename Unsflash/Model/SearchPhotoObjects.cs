using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Unsflash.Model.SearchPhoto;

namespace Unsflash.Model
{
    public class SearchPhotoObjects
    {
        //[JsonProperty("total", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        //public int total { get; set; }
        //[JsonProperty("total_pages", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        //public int total_pages { get; set; }
        //public List<Result> results { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Result> results { get; set; }
    }
}
