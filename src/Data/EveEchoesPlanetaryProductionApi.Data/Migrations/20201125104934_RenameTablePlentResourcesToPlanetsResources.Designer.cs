﻿// <auto-generated />
using EveEchoesPlanetaryProductionApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    [DbContext(typeof(EveEchoesPlanetaryProductionApiDbContext))]
    [Migration("20201125104934_RenameTablePlentResourcesToPlanetsResources")]
    partial class RenameTablePlentResourcesToPlanetsResources
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Constellation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("RegionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("RegionId");

                    b.ToTable("Constellations");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.ConstellationJump", b =>
                {
                    b.Property<long>("FromConstellationId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToConstellationId")
                        .HasColumnType("bigint");

                    b.Property<long>("FromRegionId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToRegionId")
                        .HasColumnType("bigint");

                    b.HasKey("FromConstellationId", "ToConstellationId");

                    b.HasIndex("FromRegionId");

                    b.HasIndex("ToConstellationId");

                    b.HasIndex("ToRegionId");

                    b.ToTable("ConstellationJumps");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Planet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ConstellationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("PlanetTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("RegionId")
                        .HasColumnType("bigint");

                    b.Property<long>("SolarSystemId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ConstellationId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PlanetTypeId");

                    b.HasIndex("RegionId");

                    b.HasIndex("SolarSystemId");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.PlanetResource", b =>
                {
                    b.Property<long>("PlanetId")
                        .HasColumnType("bigint");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<double>("Output")
                        .HasColumnType("float");

                    b.Property<long>("RichnessId")
                        .HasColumnType("bigint");

                    b.HasKey("PlanetId", "ItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("RichnessId");

                    b.ToTable("PlanetsResources");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.PlanetType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PlanetTypes");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Region", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.RegionJump", b =>
                {
                    b.Property<long>("FromRegionId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToRegionId")
                        .HasColumnType("bigint");

                    b.HasKey("FromRegionId", "ToRegionId");

                    b.HasIndex("ToRegionId");

                    b.ToTable("RegionJumps");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Richness", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Richnesses");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.SolarSystem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ConstellationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("RegionId")
                        .HasColumnType("bigint");

                    b.Property<double>("Security")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ConstellationId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("RegionId");

                    b.ToTable("SolarSystems");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.SolarSystemJump", b =>
                {
                    b.Property<long>("FromSolarSystemId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToSolarSystemId")
                        .HasColumnType("bigint");

                    b.Property<long>("FromConstellationId")
                        .HasColumnType("bigint");

                    b.Property<long>("FromRegionId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToConstellationId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToRegionId")
                        .HasColumnType("bigint");

                    b.HasKey("FromSolarSystemId", "ToSolarSystemId");

                    b.HasIndex("FromConstellationId");

                    b.HasIndex("FromRegionId");

                    b.HasIndex("ToConstellationId");

                    b.HasIndex("ToRegionId");

                    b.HasIndex("ToSolarSystemId");

                    b.ToTable("SolarSystemJumps");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Constellation", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Region", "Region")
                        .WithMany("Constellations")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.ConstellationJump", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Constellation", "FromConstellation")
                        .WithMany("ConstellationAsOrigins")
                        .HasForeignKey("FromConstellationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Region", "FromRegion")
                        .WithMany("JumpsFromConstellationlonghisRegion")
                        .HasForeignKey("FromRegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Constellation", "ToConstellation")
                        .WithMany("ConstellationAsDestinations")
                        .HasForeignKey("ToConstellationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Region", "ToRegion")
                        .WithMany("JumpsToConstellationlonghisRegion")
                        .HasForeignKey("ToRegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromConstellation");

                    b.Navigation("FromRegion");

                    b.Navigation("ToConstellation");

                    b.Navigation("ToRegion");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Planet", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Constellation", "Constellation")
                        .WithMany("Planets")
                        .HasForeignKey("ConstellationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.PlanetType", "PlanetType")
                        .WithMany("Planets")
                        .HasForeignKey("PlanetTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Region", "Region")
                        .WithMany("Planets")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.SolarSystem", "SolarSystem")
                        .WithMany("Planets")
                        .HasForeignKey("SolarSystemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Constellation");

                    b.Navigation("PlanetType");

                    b.Navigation("Region");

                    b.Navigation("SolarSystem");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.PlanetResource", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Item", "Item")
                        .WithMany("PlanetResources")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Planet", "Planet")
                        .WithMany("PlanetResources")
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Richness", "Richness")
                        .WithMany("PlanetResources")
                        .HasForeignKey("RichnessId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Planet");

                    b.Navigation("Richness");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.RegionJump", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Region", "FromRegion")
                        .WithMany("RegionAsOrigins")
                        .HasForeignKey("FromRegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Region", "ToRegion")
                        .WithMany("RegionAsDestinations")
                        .HasForeignKey("ToRegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromRegion");

                    b.Navigation("ToRegion");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.SolarSystem", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Constellation", "Constellation")
                        .WithMany("SolarSystems")
                        .HasForeignKey("ConstellationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Region", "Region")
                        .WithMany("SolarSystems")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Constellation");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.SolarSystemJump", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Constellation", "FromConstellation")
                        .WithMany("JumpsFromSolarSystemslonghisConstellation")
                        .HasForeignKey("FromConstellationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Region", "FromRegion")
                        .WithMany("JumpsFromSolarSystemslonghisRegion")
                        .HasForeignKey("FromRegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.SolarSystem", "FromSolarSystem")
                        .WithMany("SolarSystemAsOrigins")
                        .HasForeignKey("FromSolarSystemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Constellation", "ToConstellation")
                        .WithMany("JumpsToSolarSystemslonghisConstellation")
                        .HasForeignKey("ToConstellationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Region", "ToRegion")
                        .WithMany("JumpsToSolarSystemslonghisRegion")
                        .HasForeignKey("ToRegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.SolarSystem", "ToSolarSystem")
                        .WithMany("SolarSystemAsDestinations")
                        .HasForeignKey("ToSolarSystemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromConstellation");

                    b.Navigation("FromRegion");

                    b.Navigation("FromSolarSystem");

                    b.Navigation("ToConstellation");

                    b.Navigation("ToRegion");

                    b.Navigation("ToSolarSystem");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Constellation", b =>
                {
                    b.Navigation("ConstellationAsDestinations");

                    b.Navigation("ConstellationAsOrigins");

                    b.Navigation("JumpsFromSolarSystemslonghisConstellation");

                    b.Navigation("JumpsToSolarSystemslonghisConstellation");

                    b.Navigation("Planets");

                    b.Navigation("SolarSystems");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Item", b =>
                {
                    b.Navigation("PlanetResources");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Planet", b =>
                {
                    b.Navigation("PlanetResources");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.PlanetType", b =>
                {
                    b.Navigation("Planets");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Region", b =>
                {
                    b.Navigation("Constellations");

                    b.Navigation("JumpsFromConstellationlonghisRegion");

                    b.Navigation("JumpsFromSolarSystemslonghisRegion");

                    b.Navigation("JumpsToConstellationlonghisRegion");

                    b.Navigation("JumpsToSolarSystemslonghisRegion");

                    b.Navigation("Planets");

                    b.Navigation("RegionAsDestinations");

                    b.Navigation("RegionAsOrigins");

                    b.Navigation("SolarSystems");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Richness", b =>
                {
                    b.Navigation("PlanetResources");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.SolarSystem", b =>
                {
                    b.Navigation("Planets");

                    b.Navigation("SolarSystemAsDestinations");

                    b.Navigation("SolarSystemAsOrigins");
                });
#pragma warning restore 612, 618
        }
    }
}
