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
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly RacingAppContext _context;
        public CountryRepository(RacingAppContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteWithStoredProcedure(int id)
        {
            _context.Database.ExecuteSqlInterpolated($"[dbo].[DeleteCountry] {id}");
        }
    }
}
