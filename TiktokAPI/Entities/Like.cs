using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class Like
    {
        public long LikeId { get; set; }
        public long? UserId { get; set; }
        public long? VideoId { get; set; }
        public bool? IsDislike { get; set; }

        public virtual User? User { get; set; }
        public virtual Video? Video { get; set; }
    }
}
