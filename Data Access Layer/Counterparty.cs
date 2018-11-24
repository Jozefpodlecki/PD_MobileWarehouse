using Common;

namespace Data_Access_Layer
{
    public class Counterparty : IName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string PhoneNumber { get; set; }

        public string NIP { get; set; }
    }
}
