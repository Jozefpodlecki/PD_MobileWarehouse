using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer
{
    public class History
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Event Event { get; set; }

        public int EventId { get; set; }
    }
}
