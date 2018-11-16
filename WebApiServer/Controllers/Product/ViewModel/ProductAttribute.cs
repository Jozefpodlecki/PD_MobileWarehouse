using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiServer.Controllers.Product.ViewModel
{
    public class ProductAttribute
    {
        public Attribute Attribute { get; set; }
        public AttributeType Type { get; set; }
        public string Value { get; set; }
    }
}
