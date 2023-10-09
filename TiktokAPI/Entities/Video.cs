using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class Video
    {
        public Video()
        {
            Comments = new HashSet<Comment>();
            HashtagVideos = new HashSet<HashtagVideo>();
            Likes = new HashSet<Like>();
        }

        public long VideoId { get; set; }
        public string VideoUrl { get; set; } = null!;
        public long UserId { get; set; }
        public string? Caption { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? UploadDate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<HashtagVideo> HashtagVideos { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
