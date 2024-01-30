using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Repositories;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts.BaseApplicationService;

namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services
{
    public class PersonService : BasePersonApplicationService<SelectPersonDtoService, SelectPersonDtoService, SelectPersonDtoService, InsertPersonDtoService, DeletePersonDtoPostService, UpdatePersonDtoPostService,CreateAbstractIdPersonDtoService,GetRealIdPersonDtoService>

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
        #region [SaveAsync(InsertPersonDtoService insertPersonDtoService))]
        public async Task SaveAsync(InsertPersonDtoService insertPersonDtoService)
        {
           // _person.Id = Guid.NewGuid();
            _person.AbstractId = Guid.NewGuid().ToString();
            insertPersonDtoService.AbstractId = _person.AbstractId; 
            _person.FirstName = insertPersonDtoService.FirstName;
            _person.LastName = insertPersonDtoService.LastName;
            await _personRepository.InsertAsync(_person);
        }
        #endregion
        #region [DeleteConfirmed(DeletePersonDtoPostService deletePersonDtoPostService)]

        public async Task<bool> DeleteAsync(DeletePersonDtoPostService deletePersonDtoPostService)
        {
            var realId = GetRealId(new GetRealIdPersonDtoService { AbstractId = deletePersonDtoPostService.AbstractId });
            if (realId.HasValue)
            {
                var person = await _personRepository.SelectByIdAsync(realId.Value);
                if (person != null)
                {
                    await _personRepository.DeleteAsync(person);
                    return true;
                }
            }
            return false;
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
        #region [PopulateIdMappingsFromDatabase()]
        public void PopulateIdMappingsFromDatabase()
        {
            var mappings = _onlineShopDbContext.Person.ToDictionary(row => row.AbstractId, row => row.Id);
            _idMappings = mappings;
            Console.WriteLine($"Number of mappings retrieved: {_idMappings.Count}");

        }
        #endregion
        #region [CreateAbstractId(CreateAbstractIdPersonDtoService createAbstractIdPersonDtoService)]
        public string CreateAbstractId(CreateAbstractIdPersonDtoService createAbstractIdPersonDtoService)
        {
            var abstractId = Guid.NewGuid().ToString();
            _idMappings[abstractId] = createAbstractIdPersonDtoService.RealId ?? throw new ArgumentException("Person's Id cannot be null");
            return abstractId;

        }
        #endregion
        #region [GetRealId(GetRealIdPersonDtoService getRealIdPersonDtoService)]
        public Guid? GetRealId(GetRealIdPersonDtoService getRealIdPersonDtoService)
        {
            if (Guid.TryParse(getRealIdPersonDtoService.AbstractId, out Guid abstractGuid))
            {
                if (_idMappings.TryGetValue(getRealIdPersonDtoService.AbstractId, out var realId))
                {
                    return realId;
                }
                else
                {
                    var person = _onlineShopDbContext.Person.FirstOrDefault(p => p.AbstractId == getRealIdPersonDtoService.AbstractId);
                    if (person != null)
                    {
                        _idMappings[getRealIdPersonDtoService.AbstractId] = person.Id;
                        return person.Id;
                    }
                }
            }
            throw new ArgumentException("Invalid or missing identifier.");
        }
        #endregion
        #region [Edit(UpdatePersonDtoPostService updatePersonDtoPostService)]
        public async Task UpdateAsync(UpdatePersonDtoPostService updatePersonDtoPostService)
        {
            if (string.IsNullOrEmpty(updatePersonDtoPostService.AbstractId))
            {
                throw new ArgumentNullException(nameof(updatePersonDtoPostService.AbstractId));
            }

            var getRealIdPersonDtoService = new GetRealIdPersonDtoService
            {
                AbstractId = updatePersonDtoPostService.AbstractId
            };

            Guid? realId = GetRealId(getRealIdPersonDtoService);

            if (realId.HasValue)
            {
                var person = await _personRepository.SelectByIdAsync(realId.Value);
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
            else
            {
                throw new ArgumentException("Person real ID not found.");
            }
        }
        #endregion


    }
}