using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Note.ViewModel
{
    public class EditCounterparty : AddCounterparty
    {
        [Required]
        public int Id { get; set; }
    }
}
