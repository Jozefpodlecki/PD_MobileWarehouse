using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer
{
    public class ProductDetails
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public virtual State State { get; set; }
        public int StateId { get; set; }

        public virtual Location Location { get; set; }
        public int LocationId { get; set; }

        public int Count { get; set; }
    }
}
