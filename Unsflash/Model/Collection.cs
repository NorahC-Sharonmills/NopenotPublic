using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsflash.Model
{
    public class Collection
    {

        public class Tag
        {
            public string title { get; set; }
        }

        public class ProfileImage
        {
            public string small { get; set; }
            public string medium { get; set; }
            public string large { get; set; }
        }

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
            public int total_likes { get; set; }
            public int total_photos { get; set; }
            public int total_collections { get; set; }
            public ProfileImage profile_image { get; set; }
            public Links links { get; set; }
        }

        public class Urls
        {
            public string raw { get; set; }
            public string full { get; set; }
            public string regular { get; set; }
            public string small { get; set; }
            public string thumb { get; set; }
        }

        public class Links2
        {
            public string self { get; set; }
            public string html { get; set; }
            public string download { get; set; }
            public string download_location { get; set; }
        }

        public class CoverPhoto
        {
            public string id { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string color { get; set; }
            public int likes { get; set; }
            public bool liked_by_user { get; set; }
            public string description { get; set; }
            public User user { get; set; }
            public Urls urls { get; set; }
            public List<object> categories { get; set; }
            public Links2 links { get; set; }
        }

        public class Urls2
        {
            public string raw { get; set; }
            public string full { get; set; }
            public string regular { get; set; }
            public string small { get; set; }
            public string thumb { get; set; }
        }

        public class PreviewPhoto
        {
            public int id { get; set; }
            public Urls2 urls { get; set; }
        }

        public class ProfileImage2
        {
            public string small { get; set; }
            public string medium { get; set; }
            public string large { get; set; }
        }

        public class Links3
        {
            public string self { get; set; }
            public string html { get; set; }
            public string photos { get; set; }
            public string likes { get; set; }
            public string portfolio { get; set; }
            public string following { get; set; }
            public string followers { get; set; }
        }

        public class User2
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
            public bool followed_by_user { get; set; }
            public int total_likes { get; set; }
            public int total_photos { get; set; }
            public int total_collections { get; set; }
            public ProfileImage2 profile_image { get; set; }
            public Links3 links { get; set; }
        }

        public class Links4
        {
            public string self { get; set; }
            public string html { get; set; }
            public string photos { get; set; }
            public string related { get; set; }
        }
    }
}
