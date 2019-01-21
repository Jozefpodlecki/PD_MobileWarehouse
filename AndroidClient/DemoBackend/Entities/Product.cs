using Common;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class Product : BaseEntity, IName
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, Unique, MaxLength(50)]
        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public decimal VAT { get; set; }

        [OneToMany]
        public List<ProductAttribute> ProductAttributes { get; set; }

        [OneToMany]
        public List<ProductDetail> ProductDetails { get; set; }

        public string Barcode { get; set; }
    }
}
