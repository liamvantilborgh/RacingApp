using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RacingApp.BLL;
using RacingApp.Core.DTO_S;

namespace RacingApp.API.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : Controller
    {
        private CountryService _countryService;

        public CountriesController(CountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public IEnumerable<CountryDTO> GetAll()
        {
            return _countryService.GetAll();
        }

        [HttpGet("{id}")]
        // GET: countries/5
        public CountryDTO GetById(int id)
        {
            return _countryService.GetById(id);
        }

        [HttpPost("add")]
        //POST: countries/add
        public IActionResult Add(CountryDTO country)
        {
            _countryService.Add(country);
            return Ok(country);
        }

        [HttpPost("update/{id}")]
        //POST: countries/update/5
        public IActionResult Update(int id, CountryDTO country)
        {
            _countryService.Update(id, country);
            return Ok(country);
        }
      
        [HttpPost("delete/{id}")]
        // POST: countries/delete/5
        public IActionResult Delete(int id)
        {
            _countryService.Delete(id);
            return Ok(id);
        }
    }
}
