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
    public class TeamConfig : IEntityTypeConfiguration<Teams>
    {
        public void Configure(EntityTypeBuilder<Teams> builder)
        {
            builder
                 .HasKey(Team => Team.Id);

            builder
               .Property(Team => Team.Id)
               .UseIdentityColumn();

            builder
                .Property(Team => Team.Name)
                .IsRequired();

            builder
                .HasIndex(Team => Team.Name)
                .IsUnique();

            builder
                .ToTable("Teams");
        }
    }
}
