// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RacingApp.DAL.Data;

namespace RacingApp.DAL.Migrations
{
    [DbContext(typeof(RacingAppContext))]
    partial class RacingAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RacingApp.DAL.Entities.Circuits", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("House_Number")
                        .HasColumnType("int");

                    b.Property<int>("Length_Circuit")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Postal_Code")
                        .HasColumnType("int");

                    b.Property<string>("Street_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Circuits");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Country");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.PilotRaceTeam", b =>
                {
                    b.Property<int>("PilotId")
                        .HasColumnType("int");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("PilotId", "RaceId", "TeamId");

                    b.HasIndex("RaceId");

                    b.HasIndex("TeamId");

                    b.ToTable("PilotRaceTeam");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Pilots", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Length")
                        .HasColumnType("int");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoRelativePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("LicenseNumber")
                        .IsUnique();

                    b.ToTable("Pilots");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Races", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CircuitId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Enddate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Startdate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CircuitId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SeasonId");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Seasons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Enddate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SeriesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Startdate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SeriesId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Series", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Enddate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Sort_Order")
                        .HasColumnType("int");

                    b.Property<DateTime>("Startdate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Series");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Teams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Circuits", b =>
                {
                    b.HasOne("RacingApp.DAL.Entities.Country", "Country")
                        .WithMany("Circuits")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.PilotRaceTeam", b =>
                {
                    b.HasOne("RacingApp.DAL.Entities.Pilots", "Pilot")
                        .WithMany("PilotRaceTeam")
                        .HasForeignKey("PilotId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RacingApp.DAL.Entities.Races", "Race")
                        .WithMany("PilotRaceTeam")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RacingApp.DAL.Entities.Teams", "Team")
                        .WithMany("PilotRaceTeam")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pilot");

                    b.Navigation("Race");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Races", b =>
                {
                    b.HasOne("RacingApp.DAL.Entities.Circuits", "Circuit")
                        .WithMany("Races")
                        .HasForeignKey("CircuitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RacingApp.DAL.Entities.Seasons", "Season")
                        .WithMany("Races")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Circuit");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Seasons", b =>
                {
                    b.HasOne("RacingApp.DAL.Entities.Series", "Series")
                        .WithMany("Seasons")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Series");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Circuits", b =>
                {
                    b.Navigation("Races");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Country", b =>
                {
                    b.Navigation("Circuits");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Pilots", b =>
                {
                    b.Navigation("PilotRaceTeam");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Races", b =>
                {
                    b.Navigation("PilotRaceTeam");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Seasons", b =>
                {
                    b.Navigation("Races");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Series", b =>
                {
                    b.Navigation("Seasons");
                });

            modelBuilder.Entity("RacingApp.DAL.Entities.Teams", b =>
                {
                    b.Navigation("PilotRaceTeam");
                });
#pragma warning restore 612, 618
        }
    }
}
