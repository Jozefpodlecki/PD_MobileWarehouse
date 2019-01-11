namespace Common.DTO
{
    public class Counterparty : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public City City { get; set; }
        public string PhoneNumber { get; set; }
        public string NIP { get; set; }
    }
}
