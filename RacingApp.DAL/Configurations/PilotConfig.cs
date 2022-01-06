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
    public class PilotConfig : IEntityTypeConfiguration<Pilots>
    {
        public void Configure(EntityTypeBuilder<Pilots> builder)
        {
            builder
                .HasKey(Pilot => Pilot.Id);

            builder
               .Property(Pilot => Pilot.Id)
               .UseIdentityColumn();

            builder
                .Property(Pilot => Pilot.LicenseNumber)
                .IsRequired();

            builder
                .HasIndex(Race => Race.LicenseNumber)
                .IsUnique();

            builder
                .Property(Pilot => Pilot.Name)
                .IsRequired();

            builder
                .Property(Pilot => Pilot.FirstName)
                .IsRequired();

            builder
                .Property(Pilot => Pilot.NickName);


            builder
                .Property(Pilot => Pilot.PhotoRelativePath);

            builder
                .Property(Pilot => Pilot.Sex)
                .IsRequired();

            builder
                .Property(Pilot => Pilot.DateOfBirth)
                .IsRequired();

            builder
                .Property(Pilot => Pilot.Length);

            builder
                .Property(Pilot => Pilot.Weight);

            builder
                .ToTable("Pilots");
        }
    }
}
