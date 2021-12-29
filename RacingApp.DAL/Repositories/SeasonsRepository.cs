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
    public class SeasonsRepository : Repository<Seasons>, ISeasonsRepository
    {
        private readonly RacingAppContext _context;
        public SeasonsRepository(RacingAppContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Seasons> GetAllWithSeries()
        {
            return _context.Seasons
                .Include(s => s.Series)
                .ToList();
        }
        public IEnumerable<Seasons> GetAllBySeriesId(int seriesId)
        {
            return _context.Seasons
                .Include(s => s.Series)
                .Where(s => s.SeriesId == seriesId)
                .ToList();
        }   

        public IEnumerable<Seasons> GetByIdWithSeries(int id)
        {
            return _context.Seasons
                .Include(s => s.Series)
                .Where(s => s.Id == id)
                .ToList();
        }
    }
}
