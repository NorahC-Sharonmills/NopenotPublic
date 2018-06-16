using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsflash.Model
{
    public class SearchPhoto
    {
        public class Urls
        {
            [JsonProperty("raw", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string raw { get; set; }
            [JsonProperty("full", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string full { get; set; }
            [JsonProperty("regular", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string regular { get; set; }
            [JsonProperty("smalle", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string small { get; set; }
            [JsonProperty("thumb", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string thumb { get; set; }
        }

        public class Links
        {
            [JsonProperty("self", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string self { get; set; }
            [JsonProperty("html", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string html { get; set; }
            [JsonProperty("download", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string download { get; set; }
            [JsonProperty("download_location", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string download_location { get; set; }
        }

        public class Links2
        {
            [JsonProperty("self", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string self { get; set; }
            [JsonProperty("html", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string html { get; set; }
            [JsonProperty("photos", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string photos { get; set; }
            [JsonProperty("likes", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string likes { get; set; }
            [JsonProperty("portfolio", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string portfolio { get; set; }
            [JsonProperty("following", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string following { get; set; }
            [JsonProperty("followers", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string followers { get; set; }
        }

        public class ProfileImage
        {
            [JsonProperty("small", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string small { get; set; }
            [JsonProperty("medium", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string medium { get; set; }
            [JsonProperty("large", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string large { get; set; }
        }

        public class User
        {
            [JsonProperty("id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string id { get; set; }
            [JsonProperty("updated_at", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public DateTime updated_at { get; set; }
            [JsonProperty("username", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string username { get; set; }
            [JsonProperty("name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string name { get; set; }
            [JsonProperty("first_name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string first_name { get; set; }
            [JsonProperty("last_name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string last_name { get; set; }
            [JsonProperty("ltwitter_username", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string twitter_username { get; set; }
            [JsonProperty("portfolio_url", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string portfolio_url { get; set; }
            [JsonProperty("bio", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string bio { get; set; }
            [JsonProperty("location", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string location { get; set; }
            public Links2 links { get; set; }
            public ProfileImage profile_image { get; set; }
            [JsonProperty("instagram_username", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string instagram_username { get; set; }
            [JsonProperty("total_collections", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public int total_collections { get; set; }
            [JsonProperty("total_likes", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public int total_likes { get; set; }
            [JsonProperty("total_photos", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public int total_photos { get; set; }
        }

        public class Tag
        {
            [JsonProperty("title", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string title { get; set; }
        }

        public class PhotoTag
        {
            [JsonProperty("title", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string title { get; set; }
        }

        public class Result
        {
            [JsonProperty("id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string id { get; set; }
            [JsonProperty("created_at", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public DateTime created_at { get; set; }
            [JsonProperty("updated_at", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public DateTime updated_at { get; set; }
            [JsonProperty("width", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public int width { get; set; }
            [JsonProperty("height", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public int height { get; set; }
            [JsonProperty("color", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string color { get; set; }
            [JsonProperty("description", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string description { get; set; }
            [JsonProperty("urls", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public Urls urls { get; set; }
            public Links links { get; set; }
            [JsonProperty("categories", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public List<object> categories { get; set; }
            [JsonProperty("sponsored", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public bool sponsored { get; set; }
            [JsonProperty("likes", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public int likes { get; set; }
            [JsonProperty("liked_by_user", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public bool liked_by_user { get; set; }
            [JsonProperty("current_user_collections", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public List<object> current_user_collections { get; set; }
            [JsonProperty("slug", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public object slug { get; set; }
            public User user { get; set; }
            public List<Tag> tags { get; set; }
            public List<PhotoTag> photo_tags { get; set; }
        }
    }
}
