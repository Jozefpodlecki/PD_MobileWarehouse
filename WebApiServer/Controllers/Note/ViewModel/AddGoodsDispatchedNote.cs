using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Note.ViewModel
{
    public class AddGoodsDispatchedNote
    {
        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DispatchDate { get; set; }

        [Required]
        public Invoice Invoice { get; set; }
    }
}
