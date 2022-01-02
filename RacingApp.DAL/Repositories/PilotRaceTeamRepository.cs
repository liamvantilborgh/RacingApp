using Microsoft.EntityFrameworkCore;
using RacingApp.DAL.Data;
using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Repositories
{
    public class PilotRaceTeamRepository : Repository<PilotRaceTeam>, IPilotRaceTeamRepository
    {
        private readonly RacingAppContext _context;
        public PilotRaceTeamRepository(RacingAppContext context) : base(context)
        {
            _context = context;

        }

        public IEnumerable<PilotRaceTeam> GetByIds(int pilotId, int raceId, int teamId)
        {
            return _context.PilotRaceTeam
                .Include(e => e.Pilot)
                .Include(e => e.Race)
                .Include(e => e.Team)
               .Where(p => p.PilotId == pilotId)
               .Where(p => p.RaceId == raceId)
               .Where(p => p.TeamId == teamId);
        }

        public IEnumerable<PilotRaceTeam> GetTeamsByRaceId(int id)
        {
            return _context.PilotRaceTeam
                .Include(e => e.Race)
                .Include(e => e.Team)
                .Where(p => p.RaceId == id);
                /*.Select(p => p.Team)
                .Distinct()
                .AsEnumerable();*/
        }
    
        public IEnumerable<PilotRaceTeam> GetPilotsByRaceIdTeamId(int raceId, int teamId)
        {
           return _context.PilotRaceTeam
                .Include(e => e.Pilot)
                .Include(e => e.Race)
                .Include(e => e.Team)
                .Where(p => p.RaceId == raceId)
                .Where(p => p.TeamId == teamId);
        }

        
    }
}
