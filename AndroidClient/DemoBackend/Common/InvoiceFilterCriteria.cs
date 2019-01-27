namespace Common
{
    public class InvoiceFilterCriteria : FilterCriteria
    {
        public InvoiceType? InvoiceType { get; set; }
        public bool? AssignedToNote { get; set; }
    }
}
