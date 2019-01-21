namespace WebApiServer.Controllers.Counterparty.ViewModel
{
    public class AddCounterparty
    {
        public string Name { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public Common.DTO.City City { get; set; }

        public string PhoneNumber { get; set; }

        public string NIP { get; set; }
    }
}
