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
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
               .HasKey(Country => Country.Id);

            builder
                .Property(Country => Country.Id)
                .UseIdentityColumn();

            builder
                .Property(Country => Country.Name)
                .IsRequired();

            builder
                .HasIndex(Country => Country.Name)
                .IsUnique();
            builder
                .ToTable("Country");
        }
    }
}
