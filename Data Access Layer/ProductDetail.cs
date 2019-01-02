namespace Data_Access_Layer
{
    public class ProductDetail
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public virtual Location Location { get; set; }
        public int LocationId { get; set; }

        public int Count { get; set; }
    }
}
