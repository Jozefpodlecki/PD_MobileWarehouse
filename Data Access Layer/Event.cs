using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsEnabled { get; set; }

        public virtual ICollection<History> History { get; set; }
    }
}
