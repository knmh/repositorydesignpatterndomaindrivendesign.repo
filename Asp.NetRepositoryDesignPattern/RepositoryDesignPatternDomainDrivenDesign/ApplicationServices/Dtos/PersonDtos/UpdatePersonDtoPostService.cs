﻿namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos
{
    public class UpdatePersonDtoPostService
    {
        public string AbstractId { get; set; }
        public Guid RealId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}