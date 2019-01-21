using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Location.ViewModel
{
    public class EditLocation : AddLocation
    {
        public int Id { get; set; }
    }
}
