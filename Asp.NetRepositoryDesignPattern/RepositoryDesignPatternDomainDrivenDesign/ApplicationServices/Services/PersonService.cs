using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Repositories;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts.BaseApplicationService;

namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services
{
    public class PersonService : BasePersonApplicationService<SelectPersonDtoService, SelectPersonDtoService, SelectPersonDtoService, InsertPersonDtoService, DeletePersonDtoPostService, UpdatePersonDtoPostService>

    {
        #region [Private States]
        private readonly OnlineShopDbContext _onlineShopDbContext;
        private readonly PersonRepository<Person, Guid> _personRepository;
        private Dictionary<string, Guid?> _idMappings = new Dictionary<string, Guid?>();

        private readonly Person _person;
        #endregion
        #region [Ctor]
        public PersonService(OnlineShopDbContext onlineShopDbContext)
        {
            _onlineShopDbContext = onlineShopDbContext;
            _personRepository = new PersonRepository<Person, Guid>(_onlineShopDbContext);
            _person = new Person();

            PopulateIdMappingsFromDatabase();
        }
        #endregion


        //#region [ShowAll()]
        //public async Task<List<SelectPersonDtoService>> ShowAllAsync()
        //{
        //    var people = await _personRepository.SelectAllAsync();
        //    var selectPersonDtos = new List<SelectPersonDtoService>();

        //    foreach (var person in people)
        //    {
        //        var abstractId = CreateAbstractId(person); // Pass the Person object to CreateAbstractId method

        //        var selectPersonDto = new SelectPersonDtoService()
        //        {
        //            Id = new Guid(abstractId), // Convert the abstractId string to Guid
        //            FirstName = person.FirstName,
        //            LastName = person.LastName,
        //        };
        //        selectPersonDtos.Add(selectPersonDto);
        //    }

        //    return selectPersonDtos;
        //}
        //#endregion
        #region [SaveAsync(InsertPersonDtoService insertPersonDtoService))]
        public async Task SaveAsync(InsertPersonDtoService insertPersonDtoService)
        {
            _person.Id = Guid.NewGuid();
            _person.AbstractId = Guid.NewGuid().ToString(); // Generate a unique AbstractId
            insertPersonDtoService.AbstractId = _person.AbstractId; // Set it back to the DTO if needed
            _person.FirstName = insertPersonDtoService.FirstName;
            _person.LastName = insertPersonDtoService.LastName;
            await _personRepository.InsertAsync(_person);
        }
        #endregion
        #region [DeleteConfirmed(DeletePersonDtoPostService deletePersonDtoPostService)]

        public async Task DeleteAsync(DeletePersonDtoPostService deletePersonDtoPostService)
        {
            _person.Id = deletePersonDtoPostService.RealId;
            await _personRepository.DeleteAsync(_person);
        }
        #endregion
        #region [ShowAllAsync()]
        public async Task<List<PersonDto>> ShowAllAsync()
        {
            var people = await _personRepository.SelectAllAsync();
            var PersonDtos = new List<PersonDto>();

            foreach (var person in people)
            {
                var personDto = new PersonDto()
                {
                    RealId = person.Id,
                    AbstractId = person.AbstractId,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                };
                PersonDtos.Add(personDto);
            }

            return PersonDtos;
        }
        #endregion


        private async Task<Person> GetPersonAsync(Guid personId)
        {
            // Retrieve the person from the database using the personId
            var person = await _personRepository.SelectByIdAsync(personId);

            // If the person is not null, assign the personId value to the person.Id property
            if (person != null)
            {
                person.Id = personId;
            }

            return person;
        }


        //public async Task FillIdMapper()
        //{
        //    using (var context = new OnlineShopDbContext())
        //    {
        //        var persons = await context.Person.ToListAsync();

        //        foreach (var person in persons)
        //        {
        //            if (person.Id != null)
        //            {
        //                _idMappings.Add(person.Id.ToString(), person.Id.GetValueOrDefault());
        //            }
        //            else
        //            {
        //                throw new InvalidOperationException("Person's Id cannot be null.");
        //            }
        //        }
        //    }
        //}
        private void PopulateIdMappingsFromDatabase()
        {
            var mappings = _onlineShopDbContext.Person.ToDictionary(row => row.AbstractId, row => row.Id);
            _idMappings = mappings;

            // Add a console log to check the count of mappings
            Console.WriteLine($"Number of mappings retrieved: {_idMappings.Count}");

        }

        public string CreateAbstractId(Person person)
        {
            var abstractId = Guid.NewGuid().ToString(); // Generate a unique AbstractId
            _idMappings[abstractId] = person.Id ?? throw new ArgumentException("Person's Id cannot be null"); // Store the mapping
            return abstractId;


        }



        public Guid? GetRealId(string abstractId)
        {
            if (Guid.TryParse(abstractId, out Guid abstractGuid))
            {
                if (_idMappings.TryGetValue(abstractGuid.ToString(), out var realId))
                {
                    return realId;
                }
                else
                {
                    // Fallback to database check
                    var person = _onlineShopDbContext.Person.FirstOrDefault(p => p.AbstractId == abstractId);
                    if (person != null)
                    {
                        _idMappings[abstractId] = person.Id; // Update the dictionary
                        return person.Id;
                    }
                }
            }
            throw new ArgumentException("Invalid or missing identifier.");

        }

        #region [Edit(UpdatePersonDtoPostService updatePersonDtoPostService)]
        public async Task UpdateAsync(UpdatePersonDtoPostService updatePersonDtoPostService)
        {
            if (string.IsNullOrEmpty(updatePersonDtoPostService.AbstractId))
            {
                throw new ArgumentNullException(nameof(updatePersonDtoPostService.AbstractId));
            }

            Guid? realId = GetRealId(updatePersonDtoPostService.AbstractId);

            var person = await _personRepository.SelectByIdAsync(realId);
            if (person != null)
            {
                person.FirstName = updatePersonDtoPostService.FirstName;
                person.LastName = updatePersonDtoPostService.LastName;

                await _personRepository.UpdateAsync(person);
            }
            else
            {
                throw new ArgumentException("Person not found.");
            }
        }
        #endregion


        #region [Edit(UpdatePersonDtoGetService? updatePersonDtoGetService)]

        public async Task<UpdatePersonDtoPostService> UpdateGetAsync(UpdatePersonDtoGetService? updatePersonDtoGetService)
        {
            if (updatePersonDtoGetService?.RealId == null)
            {
                throw new ArgumentNullException(nameof(updatePersonDtoGetService.RealId));
            }

            var person = await _personRepository.SelectByIdAsync(updatePersonDtoGetService.RealId.Value);
            if (person == null)
            {
                throw new ArgumentException("Person not found.");
            }

            var abstractId = CreateAbstractId(person);

            var updatePersonDtoService = new UpdatePersonDtoPostService
            {
                AbstractId = abstractId,
                FirstName = person.FirstName,
                LastName = person.LastName
            };

            return updatePersonDtoService;
        }
        #endregion





















        //#region [Delete(DeletePersonDtoGet? deletePersonDtoGet)]
        //public async Task<DeletePersonDtoGetService> DeleteGetAsync(DeletePersonDtoGetService? deletePersonDtoGetService)
        //{
        //    _person.Id = deletePersonDtoGetService.Id;
        //     return deletePersonDtoGetService;
        //}



    }
}