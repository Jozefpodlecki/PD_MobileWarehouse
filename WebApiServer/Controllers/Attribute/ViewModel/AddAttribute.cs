using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Attribute.ViewModel
{
    public class AddAttribute
    {
        [Required]
        public string Name { get; set; }
    }
}