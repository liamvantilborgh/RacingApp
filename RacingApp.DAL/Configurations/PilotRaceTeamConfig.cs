using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Configurations
{
    public class PilotRaceTeamConfig : IEntityTypeConfiguration<PilotRaceTeam>
    {
        public void Configure(EntityTypeBuilder<PilotRaceTeam> builder)
        {
            builder
                .HasKey(PilotRaceTeam => new { PilotRaceTeam.PilotId, PilotRaceTeam.RaceId, PilotRaceTeam.TeamId });

            builder
                .HasOne(prt => prt.Pilot)
                .WithMany(pilots => pilots.PilotRaceTeam)
                .HasForeignKey(prt => prt.PilotId);

            builder
                .HasOne(prt => prt.Race)
                .WithMany(races => races.PilotRaceTeam)
                .HasForeignKey(prt => prt.RaceId);

            builder
                .HasOne(prt => prt.Team)
                .WithMany(teams => teams.PilotRaceTeam)
                .HasForeignKey(prt => prt.TeamId);

            builder
                .ToTable("PilotRaceTeam");
        }
    }
}
