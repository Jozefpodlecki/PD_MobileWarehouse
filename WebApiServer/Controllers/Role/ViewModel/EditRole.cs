using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Role.ViewModel
{
    public class EditRole : AddRole
    {
        [Required]
        public int Id { get; set; }
    }
}
