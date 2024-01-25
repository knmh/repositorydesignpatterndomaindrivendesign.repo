namespace RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.PersonDtos
{
    public class UpdatePersonDtoPost
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
