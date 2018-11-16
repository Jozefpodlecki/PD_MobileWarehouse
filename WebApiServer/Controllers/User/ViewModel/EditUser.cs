using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.User.ViewModel
{
    public class EditUser : AddUser
    {
        [Required]
        public int Id { get; set; }
    }
}
