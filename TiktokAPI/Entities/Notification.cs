using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class Notification
    {
        public long NotificationId { get; set; }
        public string? Content { get; set; }
        public bool? IsRead { get; set; }
        public long? UserId { get; set; }
        public DateTime? Time { get; set; }

        public virtual User? User { get; set; }
    }
}
