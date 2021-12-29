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
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : Controller
    {
        private TeamsService _teamsService;

        public TeamsController(TeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpGet]
        public IEnumerable<TeamsDTO> GetAll()
        {
            return _teamsService.GetAll();
        }

        [HttpGet("{id}")]
        // GET: teams/5
        public TeamsDTO GetById(int id)
        {
            return _teamsService.GetById(id);
        }

        [HttpPost("add")]
        //POST: teams/add
        public IActionResult Add(TeamsDTO country)
        {
            _teamsService.Add(country);
            return Ok(country);
        }

        [HttpPost("update/{id}")]
        //POST: teams/update/5
        public IActionResult Update(int id, TeamsDTO team)
        {
            _teamsService.Update(id, team);
            return Ok(team);
        }

        [HttpPost("delete/{id}")]
        // POST: teams/delete/5
        public IActionResult Delete(int id)
        {
            _teamsService.Delete(id);
            return Ok(id);
        }
    }
}
