using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Location.ViewModel
{
    public class EditLocation : AddLocation
    {
        [Required]
        public int Id { get; set; }
    }
}
