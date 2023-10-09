using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class HashtagVideo
    {
        public long HasTagId { get; set; }
        public long? VideoId { get; set; }
        public long Id { get; set; }

        public virtual HashTag HasTag { get; set; } = null!;
        public virtual Video? Video { get; set; }
    }
}
