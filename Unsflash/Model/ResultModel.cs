using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsflash.Model
{
    public class ResultModel
    {
            private Size size;
            public Size Size
            {
                get { return size; }
                set { size = GetSize(width, height); }
            }
            private Size GetSize(int width, int height)
            {
                Size size = new Size(width, height);
                return size;
            }
            public double Scale { get; set; }
            public string id { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string color { get; set; }
            public string description { get; set; }
            public string urlsfull { get; set; }
            public string urlsmedium { get; set; }
            public string links { get; set; }
            public List<object> categories { get; set; }
            public bool sponsored { get; set; }
            public int likes { get; set; }
            public bool liked_by_user { get; set; }
            public List<object> current_user_collections { get; set; }
            public string slug { get; set; }
            public string username { get; set; }
            public string name { get; set; }
            public string ImgmediumPro5 { get; set; }
            public bool likebyuser { get; set; }
    }
}
