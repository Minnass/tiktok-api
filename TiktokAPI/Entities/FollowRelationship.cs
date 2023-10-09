using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class FollowRelationship
    {
        public long FollowId { get; set; }
        public long? Followeduser { get; set; }
        public long? FollowerUser { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTimeOffset? Time { get; set; }

        public virtual User? FolloweduserNavigation { get; set; }
        public virtual User? FollowerUserNavigation { get; set; }
    }
}
