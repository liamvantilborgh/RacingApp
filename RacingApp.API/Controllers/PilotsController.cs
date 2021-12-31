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
    [Route("api/pilots")]
    [ApiController]
    public class PilotsController : Controller
    {
        private PilotsService _pilotsService;

        public PilotsController(PilotsService pilotsService)
        {
            _pilotsService = pilotsService;
        }

        [HttpGet]
        public IEnumerable<PilotsDTO> GetAll()
        {
            return _pilotsService.GetAll();
        }

        [HttpGet("{id}")]
        // GET: pilots/5
        public PilotsDTO GetById(int id)
        {
            return _pilotsService.GetById(id);
        }

        [HttpPost("add")]
        //POST: pilots/add
        public IActionResult Add(PilotsDTO pilot)
        {
            _pilotsService.Add(pilot);
            return Ok(pilot);
        }

        [HttpPost("update/{id}")]
        //POST: pilots/update/5
        public IActionResult Update(int id, PilotsDTO pilot)
        {
            _pilotsService.Update(id, pilot);
            return Ok(pilot);
        }

        [HttpPost("delete/{id}")]
        // POST: pilots/delete/5
        public IActionResult Delete(int id)
        {
            _pilotsService.Delete(id);
            return Ok(id);
        }
    }
}
