using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Unsflash.Model.MeInfo;

namespace Unsflash.Model
{
    public class MeRootObjects
    {
        public string id { get; set; }
        public DateTime updated_at { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public object twitter_username { get; set; }
        public object portfolio_url { get; set; }
        public string bio { get; set; }
        public string location { get; set; }
        public Links links { get; set; }
        public ProfileImage profile_image { get; set; }
        public object instagram_username { get; set; }
        public int total_collections { get; set; }
        public int total_likes { get; set; }
        public int total_photos { get; set; }
        public bool followed_by_user { get; set; }
        public List<object> photos { get; set; }
        public object badge { get; set; }
        public int downloads { get; set; }
        public Tags tags { get; set; }
        public int followers_count { get; set; }
        public int following_count { get; set; }
        public bool allow_messages { get; set; }
        public int numeric_id { get; set; }
        public string uid { get; set; }
        public int uploads_remaining { get; set; }
        public string email { get; set; }
    }
}
