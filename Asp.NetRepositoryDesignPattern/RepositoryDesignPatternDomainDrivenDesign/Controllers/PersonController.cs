using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;


namespace RepositoryDesignPatternDomainDrivenDesign.Controllers
{
    public class PersonController : Controller
    {

        #region [Private State]
        private readonly OnlineShopDbContext _onlineShopDbContext;
        private readonly ApplicationServices.Services.PersonService _personService;
        private readonly Person _person;
        private readonly ILogger<PersonController> _logger;


        #endregion
        #region [Ctor]
        public PersonController(OnlineShopDbContext onlineShopDbContext, ILogger<PersonController> logger)
        {
            _onlineShopDbContext = onlineShopDbContext;
            _personService = new PersonService(_onlineShopDbContext);
            _person = new Person();
            _logger = logger;

        }

        #endregion
        #region [async Task<IActionResult> Person()]
        // GET: PersonController
        [HttpGet]
        public async Task<IActionResult> Person()
        {
            return View();
        }
        #endregion
        #region [async Task<IActionResult> GetAllPersonJson()]
        [HttpGet]
        [Route("Person/GetPersonByIdJson")]
        public async Task<IActionResult> GetAllPersonJson()
        {
            var people = await _personService.ShowAllAsync();
            return Json(people);
        }
        #endregion
        #region [async Task<IActionResult> Create([FromBody]InsertPersonDtoService insertPersonDtoService]


        [HttpPost]
        [Route("Person/Create")]
        public async Task<IActionResult> Create([FromBody] InsertPersonDtoService insertPersonDtoService)
        {
            if (ModelState.IsValid)
            {
                await _personService.SaveAsync(insertPersonDtoService);
                return Ok(new { AbstractId = insertPersonDtoService.AbstractId }); // Return the AbstractId to the client
            }
            return View(insertPersonDtoService);
        }
        #endregion
        #region [async Task<IActionResult> DeleteConfirmed(DeletePersonDtoPost deletePersonDtoPost)]
        // POST: People/Delete/5
        [HttpPost]
        [Route("Person/DeleteConfirmed/{id}")]
        public async Task<IActionResult> DeleteConfirmed(DeletePersonDtoPostService deletePersonDtoPostService)
        {
            await _personService.DeleteAsync(deletePersonDtoPostService);
            return Ok();
        }
        #endregion
        #region [async Task<IActionResult> Edit(UpdatePersonDtoPost updatePersonDtoPost)]

        [HttpPost]
        [Route("Person/Edit/{abstractId}")]
        public async Task<IActionResult> Edit([FromBody] UpdatePersonDtoPostService updatePersonDtoPostService)
        {
            // Create an instance of GetRealIdPersonDtoService with the AbstractId
            var getRealIdDto = new GetRealIdPersonDtoService
            {
                // Assume there is a property named AbstractId in GetRealIdPersonDtoService
                AbstractId = updatePersonDtoPostService.AbstractId
            };

            // Now pass the getRealIdDto to the GetRealId method
            var realId = _personService.GetRealId(getRealIdDto);
            updatePersonDtoPostService.RealId = realId ?? Guid.Empty;

            await _personService.UpdateAsync(updatePersonDtoPostService);
            return Json(new
            {
                success = true,
                abstractId = updatePersonDtoPostService.AbstractId,
                firstName = updatePersonDtoPostService.FirstName,
                lastName = updatePersonDtoPostService.LastName
            });
        }
        #endregion
    }
}


