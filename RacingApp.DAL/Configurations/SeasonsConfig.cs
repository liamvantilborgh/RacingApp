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
    public class SeasonsConfig : IEntityTypeConfiguration<Seasons>
    {
        public void Configure(EntityTypeBuilder<Seasons> builder)
        {
            builder
                .HasKey(season => season.Id);

            builder
                .Property(season => season.Id)
                .UseIdentityColumn();

            builder
               .HasOne(season => season.Series);

            builder
                .Property(season => season.Name)
                .IsRequired();

            builder
                .HasIndex(circuit => circuit.Name)
                .IsUnique();

            builder
                .Property(season => season.Startdate)
                .IsRequired();
            //.HasDefaultValue(DateTime.Today);

            builder
                .Property(season => season.Enddate)
                .IsRequired();
                //.HasDefaultValue(DateTime.Today.AddDays(1));

            builder
                .HasOne(season => season.Series)
                .WithMany(series => series.Seasons)
                .HasForeignKey(season => season.SeriesId)
                .OnDelete(DeleteBehavior.Restrict); ;

            builder
                .ToTable("Seasons");
        }
    }
}
