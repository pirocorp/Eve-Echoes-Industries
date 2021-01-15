﻿// <auto-generated />
using System;
using EveEchoesPlanetaryProductionApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    [DbContext(typeof(EveEchoesPlanetaryProductionApiDbContext))]
    partial class EveEchoesPlanetaryProductionApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

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

                    b.ToTable("ConstellationsJumps");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("ItemTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ItemTypeId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.ItemType", b =>
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

                    b.ToTable("ItemTypes");
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

                    b.ToTable("RegionsJumps");
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

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
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

                    b.ToTable("SolarSystemsJumps");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.TargetSystem", b =>
                {
                    b.Property<string>("Jumps")
                        .HasColumnType("nvarchar(max)");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
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
                        .WithMany("JumpsFromConstellationFromThisRegion")
                        .HasForeignKey("FromRegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Constellation", "ToConstellation")
                        .WithMany("ConstellationAsDestinations")
                        .HasForeignKey("ToConstellationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Region", "ToRegion")
                        .WithMany("JumpsToConstellationFromThisRegion")
                        .HasForeignKey("ToRegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromConstellation");

                    b.Navigation("FromRegion");

                    b.Navigation("ToConstellation");

                    b.Navigation("ToRegion");
                });

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.Item", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.ItemType", "ItemType")
                        .WithMany("Items")
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ItemType");
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
                        .WithMany("JumpsFromSolarSystemsInThisRegion")
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
                        .WithMany("JumpsToSolarSystemsInThisRegion")
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.User", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.User", null)
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EveEchoesPlanetaryProductionApi.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.ItemType", b =>
                {
                    b.Navigation("Items");
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

                    b.Navigation("JumpsFromConstellationFromThisRegion");

                    b.Navigation("JumpsFromSolarSystemsInThisRegion");

                    b.Navigation("JumpsToConstellationFromThisRegion");

                    b.Navigation("JumpsToSolarSystemsInThisRegion");

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

            modelBuilder.Entity("EveEchoesPlanetaryProductionApi.Data.Models.User", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Logins");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
