using Common;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Data_Access_Layer
{
    public class Counterparty : BaseEntity, IName
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, Unique, MaxLength(50)]
        public string Name { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        [ForeignKey(typeof(City))]
        public int CityId { get; set; }

        [OneToOne]
        public City City { get; set; }

        public string PhoneNumber { get; set; }

        [NotNull, Unique, MaxLength(50)]
        public string NIP { get; set; }
    }
}
