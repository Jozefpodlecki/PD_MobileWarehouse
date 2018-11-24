namespace Client.Models
{
    public class Entry : Java.Lang.Object
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public decimal VAT { get; set; }

        public decimal Price { get; set; }
    }
}