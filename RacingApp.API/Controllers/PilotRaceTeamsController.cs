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
    [Route("api/pilotraceteam")]
    [ApiController]
    public class PilotRaceTeamsController : Controller
    {
        private PilotRaceTeamService _pilotRaceTeamService;
        public PilotRaceTeamsController(PilotRaceTeamService pilotRaceTeamService)
        {
            _pilotRaceTeamService = pilotRaceTeamService;
        }

        [HttpGet("GetByIds/{pilotId}/{raceId}/{teamId}")]
        public PilotRaceTeamDTO GetPilotRaceTeamByIds(int pilotId, int raceId, int teamId)
        {
            return _pilotRaceTeamService.GetByIDs(pilotId, raceId, teamId);
        }

        [HttpGet("GetTeamsByRaceId/{id}")]
        public IEnumerable<PilotRaceTeamDTO> GetTeamsByRaceId(int id)
        {
            return _pilotRaceTeamService.GetTeamsByRaceId(id);
        }

        [HttpGet("GetPilotsByRaceIdTeamId/{raceId}/{teamId}")]
        public IEnumerable<PilotRaceTeamDTO> GetPilotsByRaceIdTeamId(int raceId, int teamId)
        {
            return _pilotRaceTeamService.GetPilotsByRaceIdTeamId(raceId, teamId);
        }

        [HttpPost("add")]
        //POST: pilotraceteam/add
        public IActionResult Add(PilotRaceTeamDTO pilotRaceTeam)
        {
            _pilotRaceTeamService.Add(pilotRaceTeam);
            return Ok(pilotRaceTeam);
        }

        [HttpPost("delete/{pilotId}/{raceId}/{teamId}")]
        public IActionResult Delete(int pilotId, int raceId, int teamId)
        {
            _pilotRaceTeamService.Delete(pilotId, raceId, teamId);
            return Ok();
        }
    }
}
