using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class HashTag
    {
        public HashTag()
        {
            HashtagVideos = new HashSet<HashtagVideo>();
        }

        public long HashTagId { get; set; }
        public string? HashTagName { get; set; }

        public virtual ICollection<HashtagVideo> HashtagVideos { get; set; }
    }
}
