using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Role.ViewModel
{
    public class EditRole : AddRole
    {
        public int Id { get; set; }
    }
}
