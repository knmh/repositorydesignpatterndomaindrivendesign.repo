namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos
{
    public class SelectPersonDtoService
    {
        public string? AbstractId { get; set; }
        public Guid? RealId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public IEnumerable<Models.DomainModels.PersonAggregates.Person> People { get; set; }
    }
}
