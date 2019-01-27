using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Data_Access_Layer
{
    public class ProductAttribute : BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [OneToOne]
        public virtual Product Product { get; set; }

        [ForeignKey(typeof(Product))]
        public int ProductId { get; set; }

        [OneToOne]
        public Attribute Attribute { get; set; }

        [ForeignKey(typeof(Attribute))]
        public int AttributeId { get; set; }

        public string Value { get; set; }
    }
}
