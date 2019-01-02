using Common;

namespace Data_Access_Layer
{
    public class Entry : IName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public decimal VAT { get; set; }

        public decimal Price { get; set; }

        public decimal Netto => Price * Count;

        public decimal Brutto => Netto * (1 + VAT);

        public int InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
