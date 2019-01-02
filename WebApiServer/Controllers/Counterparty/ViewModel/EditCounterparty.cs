using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Counterparty.ViewModel
{
    public class EditCounterparty : AddCounterparty
    {
        [Required]
        public int Id { get; set; }
    }
}
