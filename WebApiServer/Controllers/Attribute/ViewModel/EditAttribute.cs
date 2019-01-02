using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Attribute.ViewModel
{
    public class EditAttribute : AddAttribute
    {
        [Required]
        public int Id { get; set; }
    }
}