using System;

namespace Unsflash.Model
{
    public class MainPagePhotos
    {

        public class Urls
        {
            public string raw { get; set; }
            public string full { get; set; }
            public string regular { get; set; }
            public string small { get; set; }
            public string thumb { get; set; }
        }

        public class Links
        {
            public string self { get; set; }
            public string html { get; set; }
            public string download { get; set; }
            public string download_location { get; set; }
        }

        public class Links2
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

        public class User
        {
            public string id { get; set; }
            public DateTime updated_at { get; set; }
            public string username { get; set; }
            public string name { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string twitter_username { get; set; }
            public string portfolio_url { get; set; }
            public string bio { get; set; }
            public string location { get; set; }
            public Links2 links { get; set; }
            public ProfileImage profile_image { get; set; }
            public int total_likes { get; set; }
            public int total_photos { get; set; }
            public int total_collections { get; set; }
        }
        
    }
}
