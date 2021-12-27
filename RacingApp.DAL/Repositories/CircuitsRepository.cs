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
    public class CircuitsRepository : Repository<Circuits>, ICircuitsRepository
    {
        private readonly RacingAppContext _context;
        public CircuitsRepository(RacingAppContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Circuits> GetAllWithCountry()
        {
            return _context.Circuits
                .Include(c => c.Country)
                .ToList();
        }

        public IEnumerable<Circuits> GetAllByCountryId(int countryId)
        {
            return _context.Circuits
                .Include(c => c.Country)
                .Where(c => c.CountryId == countryId)
                .ToList();
        }

        public IEnumerable<Circuits> GetByIdWithCountry(int id)
        {
            return _context.Circuits
                .Include(c => c.Country)
                .Where(c => c.Id == id)
                .ToList();
        }
    }
}
