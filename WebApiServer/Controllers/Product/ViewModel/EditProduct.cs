using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiServer.Controllers.Product.ViewModel
{
    public class EditProduct : AddProduct
    {
        public int Id { get; set; }
    }
}
