using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace Data_Access_Layer
{
    public class ProductDetail : BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [OneToOne]
        public virtual Product Product { get; set; }

        [ForeignKey(typeof(Product))]
        public int ProductId { get; set; }

        [OneToOne]
        public virtual Location Location { get; set; }

        [ForeignKey(typeof(Location))]
        public int LocationId { get; set; }

        public int Count { get; set; }
    }
}
