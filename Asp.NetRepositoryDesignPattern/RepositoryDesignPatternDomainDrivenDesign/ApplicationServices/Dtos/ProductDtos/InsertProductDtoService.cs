using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.ProductDtos
{
    public class InsertProductDtoService
    {
        public string Title { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
