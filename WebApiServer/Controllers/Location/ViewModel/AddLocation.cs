using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiServer.Controllers.Location.ViewModel
{
    public class AddLocation
    {
        [Required]
        public string Name { get; set; }
    }
}
