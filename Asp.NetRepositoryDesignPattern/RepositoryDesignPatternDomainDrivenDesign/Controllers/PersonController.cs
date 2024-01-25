using Microsoft.AspNetCore.Mvc;
using RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;

namespace RepositoryDesignPatternDomainDrivenDesign.Controllers
{
    public class PersonController : Controller
    {
        #region [Private State]
        private readonly Models.Services.Contracts.IPersonRepository<Person, Guid?> _personRepository;
        private readonly Person _person;
        #endregion

        #region [Ctor]
        public PersonController(IPersonRepository<Person, Guid?> personRepository)
        {
            _personRepository = personRepository;
            _person = new Person();
        }

        #endregion

        #region [async Task<IActionResult> Index()]
        // GET: PersonController
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var people = await _personRepository.SelectAllAsync();
            var selectPersonDtos = new List<SelectPersonDto>();

            foreach (var person in people)
            {
                var selectPersonDto = new SelectPersonDto
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName
                };

                selectPersonDtos.Add(selectPersonDto);
            }

            return View(selectPersonDtos);

        }
        #endregion

        #region [async Task<IActionResult> Create()]
        // GET: People/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        #endregion

        #region [async Task<IActionResult> Create(InsertPersonDto insertPersonDto]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsertPersonDto insertPersonDto)
        {
            // should I new person ??
            if (ModelState.IsValid)
            {
                _person.Id = null;
                _person.FirstName = insertPersonDto.FirstName;
                _person.LastName = insertPersonDto.LastName;
                await _personRepository.InsertAsync(_person);
                return RedirectToAction(nameof(Index));
            }
            return View(insertPersonDto);

        }
        #endregion

        #region [async Task<IActionResult> Delete(DeletePersonDtoGet? deletePersonDtoGet)]
        // GET: People/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(DeletePersonDtoGet? deletePersonDtoGet)
        {
            _person.Id = deletePersonDtoGet.Id;
        
            return View(deletePersonDtoGet);
        }
        #endregion

        #region [async Task<IActionResult> DeleteConfirmed(DeletePersonDtoPost deletePersonDtoPost)]
        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeletePersonDtoPost deletePersonDtoPost)
        {
            _person.Id = deletePersonDtoPost.Id;
            await _personRepository.DeleteAsync(_person);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region [async Task<IActionResult> Edit(UpdatePersonDtoPost updatePersonDtoPost)]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePersonDtoPost updatePersonDtoPost)
        {
            _person.Id = updatePersonDtoPost.Id;
            _person.FirstName = updatePersonDtoPost.FirstName;
            _person.LastName = updatePersonDtoPost.LastName;
            await _personRepository.UpdateAsync(_person);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region [async Task<IActionResult> Edit(UpdatePersonDtoGet? updatePersonDtoGet)]
        // GET: People/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(UpdatePersonDtoGet? updatePersonDtoGet)
        {
            if (updatePersonDtoGet.Id == null)
            {
                throw new ArgumentNullException(nameof(updatePersonDtoGet.Id));
            }

            var person = await _personRepository.SelectByIdAsync(updatePersonDtoGet.Id);
            if (person == null)
            {
                throw new ArgumentException("Person not found");
            }
            var updatePersonDto = new UpdatePersonDtoPost
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName
            };

            return View("Edit", updatePersonDto);
        }
        #endregion
    }
}
