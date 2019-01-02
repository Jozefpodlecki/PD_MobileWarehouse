using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Note.ViewModel
{
    public class Entry
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal VAT { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? ProductId { get; set; }

    }
}
