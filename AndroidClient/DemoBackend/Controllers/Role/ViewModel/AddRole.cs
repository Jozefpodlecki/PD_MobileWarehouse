using Common.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Role.ViewModel
{
    public class AddRole
    {
        public string Name { get; set; }

        public List<Claim> Claims { get; set; }
    }
}
