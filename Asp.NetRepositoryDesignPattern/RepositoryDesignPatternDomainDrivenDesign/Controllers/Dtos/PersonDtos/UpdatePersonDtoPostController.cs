namespace RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.PersonDtos
{
    public class UpdatePersonDtoPostController
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
