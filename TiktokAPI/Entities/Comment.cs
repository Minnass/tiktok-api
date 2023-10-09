using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class Comment
    {
        public long CommentId { get; set; }
        public long? UserId { get; set; }
        public long? VideoId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? Time { get; set; }
        public string? Text { get; set; }

        public virtual User? User { get; set; }
        public virtual Video? Video { get; set; }
    }
}
