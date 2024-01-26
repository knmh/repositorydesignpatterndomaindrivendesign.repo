using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;


namespace RepositoryDesignPatternDomainDrivenDesign.Controllers
{
    public class PersonController : Controller
    {
        #region [Private State]
        private readonly ApplicationServices.Services.PersonService _personService;
        private readonly Person _person;
      
        #endregion

        #region [Ctor]
        public PersonController(PersonService personService)
        {
            _personService = personService;
            _person = new Person();
           
        }

        #endregion
        #region [async Task<IActionResult> Index()]
        // GET: PersonController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var selectPersonDtos = await _personService.ShowAll();
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

        #region [async Task<IActionResult> Create(InsertPersonDtoService insertPersonDtoService)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsertPersonDtoService insertPersonDtoService)
        {
            if (ModelState.IsValid)
            {
                await _personService.Save(insertPersonDtoService);
                return RedirectToAction(nameof(Index));
            }
            return View(insertPersonDtoService);
        }
        #endregion

        #region [async Task<IActionResult> Delete(DeletePersonDtoGetService? deletePersonDtoGetService)]
        // GET: People/Delete/5
        [HttpGet]

        public async Task<IActionResult> Delete(DeletePersonDtoGetService? deletePersonDtoGetService)
        {

            return View(deletePersonDtoGetService);
        }
        #endregion

        #region [async Task<IActionResult> DeleteConfirmed(DeletePersonDtoPostController deletePersonDtoPostController)]
        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeletePersonDtoPostService deletePersonDtoPostService)
        {
            await _personService.DeleteConfirmed(deletePersonDtoPostService);
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region [async Task<IActionResult> Edit(UpdatePersonDtoPostController updatePersonDtoPostController)]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePersonDtoPostService updatePersonDtoPostService)
        {
            await _personService.Edit(updatePersonDtoPostService);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region [async Task<IActionResult> Edit(UpdatePersonDtoGet? updatePersonDtoGet)]
        // GET: People/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(UpdatePersonDtoGetService? updatePersonDtoGetService)
        {

            var person = await _personService.Edit(updatePersonDtoGetService);
            return View(person);

        }
        #endregion
    }
}
