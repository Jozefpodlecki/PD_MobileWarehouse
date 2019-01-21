namespace Common.DTO
{
    public class ProductAttribute : BaseEntity
    {
        public Attribute Attribute { get; set; }
        public string Value { get; set; }
    }
}
