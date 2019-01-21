using Common;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class Location : BaseEntity, IName
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, Unique, MaxLength(50)]
        public string Name { get; set; }

        [OneToMany]
        public virtual List<ProductDetail> ProductDetails { get; set; }
    }
}
