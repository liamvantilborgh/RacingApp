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
    public class RacesRepository : Repository<Races>, IRacesRepository
    {
        private readonly RacingAppContext _context;
        public RacesRepository(RacingAppContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Races> GetAllWithSeasonsCircuits()
        {
            return _context.Races
                .Include(r => r.Season)
                .Include(r => r.Circuit)
                .ToList();
        }

        public IEnumerable<Races> GetAllByCircuitsId(int circuitId)
        {
            return _context.Races
                .Include(r => r.Season)
                .Include(r => r.Circuit)
                .Where(r => r.CircuitId == circuitId)
                .ToList();

        }

        public IEnumerable<Races> GetAllBySeasonsId(int seasonId)
        {
            return _context.Races
                .Include(r => r.Season)
                .Include(r => r.Circuit)
                .Where(r => r.SeasonId == seasonId)
                .ToList();
        }

        public IEnumerable<Races> GetAllBySeasonsIdCircuitsId(int seasonId, int circuitId)
        {
            return _context.Races
                .Include(r => r.Season)
                .Include(r => r.Circuit)
                .Where(r => r.SeasonId == seasonId)
                .Where(r => r.CircuitId == circuitId)
                .ToList();
        }      

        public IEnumerable<Races> GetByIdWithSeasonsCircuits(int id)
        {
            return _context.Races
                .Include(r => r.Season)
                .Include(r => r.Circuit)
                .Where(r => r.Id == id)
                .ToList();
        }
    }
}
