using Microsoft.EntityFrameworkCore;
using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.DAL.Configurations
{
    public class RaceConfig : IEntityTypeConfiguration<Races>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Races> builder)
        {
            builder
                .HasKey(Race => Race.Id);

            builder
               .Property(Race => Race.Id)
               .UseIdentityColumn();

            builder
               .HasOne(Race => Race.Season);

            builder
               .HasOne(Race => Race.Circuit);

            builder
                .Property(Race => Race.Name)
                .IsRequired();

            builder
                .HasIndex(Race => Race.Name)
                .IsUnique();

            builder
                .Property(Race => Race.Startdate)
                .IsRequired();

            builder
                .Property(Race => Race.Enddate)
                .IsRequired();

            builder
                .HasOne(Race => Race.Season)
                .WithMany(Season => Season.Races)
                .HasForeignKey(Race => Race.SeasonId);

            builder
                .HasOne(Race => Race.Circuit)
                .WithMany(Circuit => Circuit.Races)
                .HasForeignKey(Race => Race.CircuitId);

            builder
                .ToTable("Races");
        }
    }
}
