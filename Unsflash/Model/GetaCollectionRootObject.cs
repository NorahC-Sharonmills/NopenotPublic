using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Unsflash.Model.GetaCollection;

namespace Unsflash.Model
{
    public class GetaCollectionRootObject
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime published_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool curated { get; set; }
        public bool featured { get; set; }
        public int total_photos { get; set; }
        public bool @private { get; set; }
        public string share_key { get; set; }
        public List<Tag> tags { get; set; }
        public CoverPhoto cover_photo { get; set; }
        public List<PreviewPhoto> preview_photos { get; set; }
        public User2 user { get; set; }
        public Links4 links { get; set; }
    }
}
