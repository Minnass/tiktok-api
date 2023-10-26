using System;
using System.Collections.Generic;

namespace TiktokAPI.Entities
{
    public partial class Search
    {
        public long SearchId { get; set; }
        public long? UserId { get; set; }
        public string? KeyWord { get; set; }

        public virtual User? User { get; set; }
    }
}
