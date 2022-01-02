using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RacingApp.DAL.Configurations;
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
        public DbSet<Circuits> Circuits { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Seasons> Seasons { get; set; }
        public DbSet<Races> Races { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Pilots> Pilots { get; set; }
        public DbSet<PilotRaceTeam> PilotRaceTeam { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {     
            builder
                .ApplyConfiguration(new PilotRaceTeamConfig());

            builder
                .ApplyConfiguration(new CircuitConfig());

            builder
                .ApplyConfiguration(new SeasonsConfig());
        }
    }
}
