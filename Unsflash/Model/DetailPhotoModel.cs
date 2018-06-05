using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsflash.Model
{
    public class DetailPhotoModel
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
            public int total_collections { get; set; }
            public int total_likes { get; set; }
            public int total_photos { get; set; }
        }

        public class Position
        {
            [JsonProperty("latitude", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public double latitude { get; set; }
            [JsonProperty("longitude", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public double longitude { get; set; }
        }

        public class Location
        {
            [JsonProperty("title", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string title { get; set; }
            [JsonProperty("name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string name { get; set; }
            [JsonProperty("city", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public object city { get; set; }
            [JsonProperty("country", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string country { get; set; }
            public Position position { get; set; }
        }

        public class Exif
        {
            [JsonProperty("make", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string make { get; set; }
            [JsonProperty("model", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string model { get; set; }
            [JsonProperty("exposure_time", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string exposure_time { get; set; }
            [JsonProperty("aperture", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string aperture { get; set; }
            [JsonProperty("focal_length", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public string focal_length { get; set; }
            [JsonProperty("iso", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
            public int iso { get; set; }
        }

        public class RootObject
        {
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
            public bool sponsored { get; set; }
            public int likes { get; set; }
            public User user { get; set; }
            public List<object> current_user_collections { get; set; }
            public object slug { get; set; }
            public Location location { get; set; }
            public Exif exif { get; set; }
            public int views { get; set; }
            public int downloads { get; set; }
        }
    }
}
