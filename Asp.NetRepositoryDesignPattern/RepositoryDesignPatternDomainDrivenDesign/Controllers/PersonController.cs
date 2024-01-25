using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services;
using RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;


namespace RepositoryDesignPatternDomainDrivenDesign.Controllers
{
    public class PersonController : Controller
    {
        #region [Private State]
        private readonly ApplicationServices.Services.PersonService _personService;
        private readonly Person _person;
        private readonly IMapper _mapper;
        #endregion

        #region [Ctor]
        public PersonController(PersonService personService, IMapper mapper)
        {
            _personService = personService;
            _person = new Person();
            _mapper = mapper;
        }

        #endregion
        #region [async Task<IActionResult> Index()]
        // GET: PersonController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var selectPersonDtos = await _personService.ShowAll();
            var selectPersonDtoController = new SelectPersonDtoController
            {
                People = _mapper.Map<List<SelectPersonDtoService>>(selectPersonDtos)
            };
            return View(selectPersonDtoController);
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
        public async Task<IActionResult> Create(InsertPersonDtoController insetPersonDtoController)
        {
            if (ModelState.IsValid)
            {

                var insertPersonDtoGet = _mapper.Map<InsertPersonDtoService>(insetPersonDtoController);
                await _personService.Save(insertPersonDtoGet);
                return RedirectToAction(nameof(Index));
            }
            return View(insetPersonDtoController);
        }
        #endregion

        #region [async Task<IActionResult> Delete(DeletePersonDtoGet? deletePersonDtoGet)]
        // GET: People/Delete/5
        [HttpGet]

        public async Task<IActionResult> Delete(DeletePersonDtoGetController? deletePersonDtoGetController)
        {

            return View(deletePersonDtoGetController);
        }
        #endregion

        #region [async Task<IActionResult> DeleteConfirmed(DeletePersonDtoPost deletePersonDtoPost)]
        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeletePersonDtoPostController deletePersonDtoPostController)
        {
            var deletePersonDtoPost = _mapper.Map<DeletePersonDtoPostService>(deletePersonDtoPostController);
            await _personService.DeleteConfirmed(deletePersonDtoPost);
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region [async Task<IActionResult> Edit(UpdatePersonDtoPost updatePersonDtoPost)]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePersonDtoPostController updatePersonDtoPostController)
        {
            var updatePersonDtoPost = _mapper.Map<UpdatePersonDtoPostService>(updatePersonDtoPostController);
            await _personService.Edit(updatePersonDtoPost);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region [async Task<IActionResult> Edit(UpdatePersonDtoGet? updatePersonDtoGet)]
        // GET: People/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(UpdatePersonDtoGetController? updatePersonDtoGetController)
        {

            var updatePersonDtoGet = _mapper.Map<UpdatePersonDtoGetService>(updatePersonDtoGetController);
            var person = await _personService.Edit(updatePersonDtoGet);
            var updatePersonDtoPostController = _mapper.Map<UpdatePersonDtoPostController>(person);
            return View(updatePersonDtoPostController);

        }
        #endregion
    }
}
