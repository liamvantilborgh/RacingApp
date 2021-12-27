using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RacingApp.BLL;
using RacingApp.DAL.Data;
using RacingApp.DAL.Entities;

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
        public IEnumerable<Country> GetAllCountries()
        {
            return _countryService.GetAllCountries();
        }
    }
}
