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
    public class SeriesConfig : IEntityTypeConfiguration<Series>
    {
        public void Configure(EntityTypeBuilder<Series> builder)
        {
            builder
                .HasKey(Series=> Series.Id);

            builder
                .Property(Series => Series.Id)
                .UseIdentityColumn();

            builder
                .Property(Series => Series.Name)
                .IsRequired();

            builder
                .HasIndex(Series => Series.Name)
                .IsUnique();

            builder
                .Property(Series => Series.Startdate)
                .IsRequired();

            builder
                .Property(Series => Series.Enddate);

            builder
                .ToTable("Series");
        }
    }
}
