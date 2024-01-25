using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;

namespace RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.PersonDtos
{
    public class SelectPersonDtoController
    {
        //public Guid? Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public IEnumerable<SelectPersonDtoService> People { get; set; }
    }
}
