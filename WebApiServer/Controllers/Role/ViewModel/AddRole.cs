using Common.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Role.ViewModel
{
    public class AddRole
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<Claim> Claims { get; set; }
    }
}
