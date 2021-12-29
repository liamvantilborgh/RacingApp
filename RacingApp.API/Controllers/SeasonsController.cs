using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RacingApp.BLL;
using RacingApp.Core.DTO_S;
using RacingApp.DAL.Data;
using RacingApp.DAL.Entities;

namespace RacingApp.API.Controllers
{
    [Route("api/seasons")]
    [ApiController]
    public class SeasonsController : Controller
    {
        private SeasonsService _seasonsService;

        public SeasonsController(SeasonsService seasonsService)
        {
            _seasonsService = seasonsService;
        }

        // GET: api/seasons
        [HttpGet]
        public IEnumerable<SeasonsDTO> GetAll()
        {
            return _seasonsService.GetAll();
        }

        [HttpGet("{id}")]
        // GET: seasons/5
        public SeasonsDTO GetById(int id)
        {
            return _seasonsService.GetById(id);
        }

        [HttpGet("getAllBySeriesId/{id}")]
        public IEnumerable<SeasonsDTO> GetAllBySeriesId(int id)
        {
            return _seasonsService.GetAllBySeriesId(id);
        }

        [HttpPost("add")]
        public IActionResult Add(SeasonsDTO season)
        {
            _seasonsService.Add(season);

            return Ok(season);
        }

        [HttpPost("update/{id}")]
        public IActionResult Update(int id, SeasonsDTO season)
        {
            _seasonsService.Update(id, season);
            return Ok(season);
        }

        [HttpPost("delete/{id}")]
        // POST: seasons/delete/5
        public IActionResult Delete(int id)
        {
            _seasonsService.Delete(id);
            return Ok(id);
        }
    }
}
