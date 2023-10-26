using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            FollowRelationshipFolloweduserNavigations = new HashSet<FollowRelationship>();
            FollowRelationshipFollowerUserNavigations = new HashSet<FollowRelationship>();
            Likes = new HashSet<Like>();
            Notifications = new HashSet<Notification>();
            RefreshTokens = new HashSet<RefreshToken>();
            Searches = new HashSet<Search>();
            Videos = new HashSet<Video>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public string? Salt { get; set; }
        public string? Email { get; set; }
        public string? DisplayedName { get; set; }
        public string? Bio { get; set; }
        public string? Avatar { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FollowRelationship> FollowRelationshipFolloweduserNavigations { get; set; }
        public virtual ICollection<FollowRelationship> FollowRelationshipFollowerUserNavigations { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<Search> Searches { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
