using RacingApp.DAL.Data;
using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Repositories
{
    public class SeriesRepository : Repository<Series>, ISeriesRepository
    {
        private readonly RacingAppContext _context;
        public SeriesRepository(RacingAppContext context) : base(context)
        {
            _context = context;
        }
    }
}
