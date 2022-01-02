using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Repositories
{
    public interface IPilotRaceTeamRepository : IRepository<PilotRaceTeam>
    {
        IEnumerable<PilotRaceTeam> GetByIds(int pilotId, int raceId, int teamId);
        IEnumerable<PilotRaceTeam> GetTeamsByRaceId(int id);
        IEnumerable<PilotRaceTeam> GetPilotsByRaceIdTeamId(int raceId, int teamId);
    }
}
