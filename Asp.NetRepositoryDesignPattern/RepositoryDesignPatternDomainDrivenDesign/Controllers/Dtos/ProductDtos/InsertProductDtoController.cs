using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.ProductDtos
{
    public class InsertProductDtoController
    {
        public string Title { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
