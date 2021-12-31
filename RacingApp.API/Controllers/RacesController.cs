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
    [Route("api/races")]
    [ApiController]
    public class RacesController : Controller
    {
        private RacesService _racesService;
        public RacesController(RacesService racesService)
        {
            _racesService = racesService;
        }

        // GET: api/races
        [HttpGet]
        public IEnumerable<RacesDTO> GetAll()
        {
            return _racesService.GetAll();
        }

        [HttpGet("{id}")]
        // GET: races/5
        public RacesDTO GetById(int id)
        {
            return _racesService.GetById(id);
        }

        [HttpGet("getAllBySeasonId/{id}")]
        public IEnumerable<RacesDTO> GetAllBySeasonId(int id)
        {
            return _racesService.getAllBySeasonId(id);
        }

        [HttpGet("getAllByCircuitId/{id}")]
        public IEnumerable<RacesDTO> GetAllByCircuitId(int id)
        {
            return _racesService.GetAllByCircuitId(id);
        }

        [HttpGet("getAllByBothId/{seasonId}/{circuitId}")]
        public IEnumerable<RacesDTO> GetAllByBothId(int seasonId, int circuitId)
        {
            return _racesService.GetAllByBothId(seasonId, circuitId);
        }

        [HttpPost("add")]
        public IActionResult Add(RacesDTO race)
        {
            _racesService.Add(race);

            return Ok(race);
        }

        [HttpPost("update/{id}")]
        public IActionResult Update(int id, RacesDTO race)
        {
            _racesService.Update(id, race);
            return Ok(race);
        }

        [HttpPost("delete/{id}")]
        // POST: races/delete/5
        public IActionResult Delete(int id)
        {
            _racesService.Delete(id);
            return Ok(id);
        }
    }
}
