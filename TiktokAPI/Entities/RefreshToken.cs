using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class RefreshToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string? Token { get; set; }
        public bool? IsUsed { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? IssuedDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
