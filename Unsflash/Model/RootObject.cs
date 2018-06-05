using System;
using System.Collections.Generic;
using Windows.Foundation;
using static Unsflash.Model.MainPagePhotos;

namespace Unsflash.Model
{
    public class RootObject
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
        public object description { get; set; }
        public List<object> categories { get; set; }
        public Urls urls { get; set; }
        public Links links { get; set; }
        public bool liked_by_user { get; set; }
        public int likes { get; set; }
        public User user { get; set; }
        public List<object> current_user_collections { get; set; }
    }  
}
