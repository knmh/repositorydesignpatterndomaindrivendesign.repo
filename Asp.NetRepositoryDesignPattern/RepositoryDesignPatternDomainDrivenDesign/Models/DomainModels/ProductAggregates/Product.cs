namespace RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates
{
    public class Product
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        // public byte[] Picture { get; set; }
        // public string PicturePath { get; set; }
    }
}
