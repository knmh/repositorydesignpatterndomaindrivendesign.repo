using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.ProductDtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.ProductDtos
{
    public class SelectProductDtoController
    {
        //public Guid? Id { get; set; }
        //public string Title { get; set; }
        // [Column(TypeName = "decimal(18,3)")]
        //public decimal UnitPrice { get; set; }
        //public int Quantity { get; set; }
        public IEnumerable<SelectProductDtoService> Products { get; set; }
    }
}
