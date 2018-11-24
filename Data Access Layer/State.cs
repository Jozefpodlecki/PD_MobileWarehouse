using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer
{
    public class State : IName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ProductDetails> ProductDetails { get; set; }
    }
}
