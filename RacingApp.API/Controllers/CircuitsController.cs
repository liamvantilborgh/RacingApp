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
    [Route("api/circuits")]
    [ApiController]
    public class CircuitsController : Controller
    {
        private CircuitsService _circuitsService;

        public CircuitsController(CircuitsService circuitsService)
        {
            _circuitsService = circuitsService;
        }

        // GET: api/circuits
        [HttpGet]
        public IEnumerable<CircuitsDTO> GetAll()
        {
            return _circuitsService.GetAll();
        }

        [HttpGet("{id}")]
        // GET: circuits/5
        public CircuitsDTO GetById(int id)
        {
            return _circuitsService.GetById(id);
        }

        [HttpGet("getAllByCountryId/{id}")]
        public IEnumerable<CircuitsDTO> GetAllByCountryId(int id)
        {
            return _circuitsService.GetAllByCountryId(id);
        }

        [HttpPost("add")]
        public IActionResult Add(CircuitsDTO circuit)
        {
            _circuitsService.Add(circuit);

            return Ok(circuit);
        }

        [HttpPost("update/{id}")]
        public IActionResult UpdatePerson(int id, CircuitsDTO circuit)
        {
            _circuitsService.Update(id, circuit);
            return Ok(circuit);
        }
    }
}
