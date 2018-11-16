using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Note.ViewModel
{
    public class AddGoodsReceivedNote
    {
        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime ReceiveDate { get; set; }

        [Required]
        public Invoice Invoice { get; set; }
    }
}
