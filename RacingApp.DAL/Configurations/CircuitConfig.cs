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
    public class CircuitConfig : IEntityTypeConfiguration<Circuits>
    {
        public void Configure(EntityTypeBuilder<Circuits> builder)
        {
            builder
                .HasKey(circuit => circuit.Id);

            builder
                .Property(circuit => circuit.Id)
                .UseIdentityColumn();

            builder
               .HasOne(circuit => circuit.Country);

            builder
                .Property(circuit => circuit.Name)
                .IsRequired();

            builder
                .HasIndex(circuit => circuit.Name)
                .IsUnique();

            builder
                .Property(circuit => circuit.Length_Circuit)
                .IsRequired();

            builder
                .Property(circuit => circuit.Street_Name)
                .IsRequired();

            builder
                .Property(circuit => circuit.House_Number)
                .IsRequired();

            builder
                .Property(circuit => circuit.City)
                .IsRequired();

            builder
                .Property(circuit => circuit.Postal_Code)
                .IsRequired();

            builder
                .HasOne(circuit => circuit.Country)
                .WithMany(country => country.Circuits)
                .HasForeignKey(circuit => circuit.CountryId);

            builder
                .ToTable("Circuits");
        }
    }
}
