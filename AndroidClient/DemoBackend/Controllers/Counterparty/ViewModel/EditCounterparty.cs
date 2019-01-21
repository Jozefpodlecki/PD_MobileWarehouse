using System.ComponentModel.DataAnnotations;

namespace WebApiServer.Controllers.Counterparty.ViewModel
{
    public class EditCounterparty : AddCounterparty
    {
        public int Id { get; set; }
    }
}
