using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Repositories;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;

namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services
{
    public class PersonService
    {
        #region [Private States]
        private readonly OnlineShopDbContext _onlineShopDbContext;
        private readonly PersonRepository<Person, Guid> _personRepository;
        private readonly Person _person;
        #endregion

        #region [Ctor]
        public PersonService(OnlineShopDbContext onlineShopDbContext)
        {
            _onlineShopDbContext = onlineShopDbContext;
            _personRepository = new PersonRepository<Person, Guid>(_onlineShopDbContext);
            _person = new Person();
        }
        #endregion

        #region [ShowAll()]
        public async Task<List<SelectPersonDtoService>> ShowAll()
        {
            var people = await _personRepository.SelectAllAsync();
            var selectPersonDtos = new List<SelectPersonDtoService>();

            foreach (var person in people)
            {
                var selectPersonDto = new SelectPersonDtoService()
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                };
                selectPersonDtos.Add(selectPersonDto);
            }

            return selectPersonDtos;
        }
        #endregion

        #region [Save(InsertPersonDtoService insertPersonDtoService)]
        public async Task Save(InsertPersonDtoService insertPersonDtoService)
        {
            _person.Id = null;
            _person.FirstName = insertPersonDtoService.FirstName;
            _person.LastName = insertPersonDtoService.LastName;
            await _personRepository.InsertAsync(_person);
        }
        #endregion

        #region [Delete(DeletePersonDtoGetService? deletePersonDtoGetService)]
        public async Task<DeletePersonDtoGetService> Delete(DeletePersonDtoGetService? deletePersonDtoGetService)
        {

            _person.Id = deletePersonDtoGetService.Id;
            return deletePersonDtoGetService;
        }
        #endregion

        #region [DeleteConfirmed(DeletePersonDtoPostService deletePersonDtoPostService)]

        public async Task DeleteConfirmed(DeletePersonDtoPostService deletePersonDtoPostService)
        {
            _person.Id = deletePersonDtoPostService.Id;
            await _personRepository.DeleteAsync(_person);
        }
        #endregion

        #region [ Edit(UpdatePersonDtoPostService updatePersonDtoPostService)]
        public async Task Edit(UpdatePersonDtoPostService updatePersonDtoPostService)
        {
            _person.Id = updatePersonDtoPostService.Id;
            _person.FirstName = updatePersonDtoPostService.FirstName;
            _person.LastName = updatePersonDtoPostService.LastName;
            await _personRepository.UpdateAsync(_person);
        }
        #endregion

        #region [Edit(UpdatePersonDtoGetService? updatePersonDtoGetService)]

        public async Task<UpdatePersonDtoPostService> Edit(UpdatePersonDtoGetService? updatePersonDtoGetService)
        {
            if (updatePersonDtoGetService.Id == null)
            {
                throw new ArgumentNullException(nameof(updatePersonDtoGetService.Id));
            }

            var person = await _personRepository.SelectByIdAsync(updatePersonDtoGetService.Id);
            if (person == null)
            {
                throw new ArgumentException("Person not found");
            }
            var updatePersonDtoService = new UpdatePersonDtoPostService
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName
            };

            return updatePersonDtoService;
        }
        #endregion
    }
}