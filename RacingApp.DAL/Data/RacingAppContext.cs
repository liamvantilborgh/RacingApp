using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RacingApp.DAL.Entities;

namespace RacingApp.DAL.Data
{
    public class RacingAppContext : DbContext
    {
        public RacingAppContext (DbContextOptions<RacingAppContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Country { get; set; }
    }
}
