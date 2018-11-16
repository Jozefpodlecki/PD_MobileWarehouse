using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Note.ViewModel
{
    public class AddCounterparty
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public City City { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string NIP { get; set; }
    }
}
