using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class Feedback
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string? Title { get; set; }
        public string? Problem { get; set; }
        public string? Image { get; set; }

        public virtual User? User { get; set; }
    }
}
