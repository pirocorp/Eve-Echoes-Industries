namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanetTypesCsvFilePath",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Richnesses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Richnesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constellations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constellations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constellations_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegionJumps",
                columns: table => new
                {
                    FromRegionId = table.Column<long>(type: "bigint", nullable: false),
                    ToRegionId = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionJumps", x => new { x.FromRegionId, x.ToRegionId });
                    table.ForeignKey(
                        name: "FK_RegionJumps_Regions_FromRegionId",
                        column: x => x.FromRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegionJumps_Regions_ToRegionId",
                        column: x => x.ToRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConstellationJumps",
                columns: table => new
                {
                    FromConstellationId = table.Column<long>(type: "bigint", nullable: false),
                    ToConstellationId = table.Column<long>(type: "bigint", nullable: false),
                    FromRegionId = table.Column<long>(type: "bigint", nullable: false),
                    ToRegionId = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstellationJumps", x => new { x.FromConstellationId, x.ToConstellationId });
                    table.ForeignKey(
                        name: "FK_ConstellationJumps_Constellations_FromConstellationId",
                        column: x => x.FromConstellationId,
                        principalTable: "Constellations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConstellationJumps_Constellations_ToConstellationId",
                        column: x => x.ToConstellationId,
                        principalTable: "Constellations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConstellationJumps_Regions_FromRegionId",
                        column: x => x.FromRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConstellationJumps_Regions_ToRegionId",
                        column: x => x.ToRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolarSystems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Security = table.Column<double>(type: "float", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    ConstellationId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolarSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolarSystems_Constellations_ConstellationId",
                        column: x => x.ConstellationId,
                        principalTable: "Constellations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolarSystems_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    ConstellationId = table.Column<long>(type: "bigint", nullable: false),
                    SolarSystemId = table.Column<long>(type: "bigint", nullable: false),
                    PlanetTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planets_Constellations_ConstellationId",
                        column: x => x.ConstellationId,
                        principalTable: "Constellations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planets_PlanetTypes_PlanetTypeId",
                        column: x => x.PlanetTypeId,
                        principalTable: "PlanetTypesCsvFilePath",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planets_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planets_SolarSystems_SolarSystemId",
                        column: x => x.SolarSystemId,
                        principalTable: "SolarSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolarSystemJumps",
                columns: table => new
                {
                    FromSolarSystemId = table.Column<long>(type: "bigint", nullable: false),
                    ToSolarSystemId = table.Column<long>(type: "bigint", nullable: false),
                    FromRegionId = table.Column<long>(type: "bigint", nullable: false),
                    FromConstellationId = table.Column<long>(type: "bigint", nullable: false),
                    ToConstellationId = table.Column<long>(type: "bigint", nullable: false),
                    ToRegionId = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolarSystemJumps", x => new { x.FromSolarSystemId, x.ToSolarSystemId });
                    table.ForeignKey(
                        name: "FK_SolarSystemJumps_Constellations_FromConstellationId",
                        column: x => x.FromConstellationId,
                        principalTable: "Constellations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolarSystemJumps_Constellations_ToConstellationId",
                        column: x => x.ToConstellationId,
                        principalTable: "Constellations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolarSystemJumps_Regions_FromRegionId",
                        column: x => x.FromRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolarSystemJumps_Regions_ToRegionId",
                        column: x => x.ToRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolarSystemJumps_SolarSystems_FromSolarSystemId",
                        column: x => x.FromSolarSystemId,
                        principalTable: "SolarSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolarSystemJumps_SolarSystems_ToSolarSystemId",
                        column: x => x.ToSolarSystemId,
                        principalTable: "SolarSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanetResources",
                columns: table => new
                {
                    PlanetId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    RichnessId = table.Column<long>(type: "bigint", nullable: false),
                    Output = table.Column<double>(type: "float", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanetResources", x => new { x.PlanetId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_PlanetResources_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanetResources_Planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanetResources_Richnesses_RichnessId",
                        column: x => x.RichnessId,
                        principalTable: "Richnesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstellationJumps_FromRegionId",
                table: "ConstellationJumps",
                column: "FromRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstellationJumps_ToConstellationId",
                table: "ConstellationJumps",
                column: "ToConstellationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstellationJumps_ToRegionId",
                table: "ConstellationJumps",
                column: "ToRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Constellations_Name",
                table: "Constellations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Constellations_RegionId",
                table: "Constellations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanetResources_ItemId",
                table: "PlanetResources",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanetResources_RichnessId",
                table: "PlanetResources",
                column: "RichnessId");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_ConstellationId",
                table: "Planets",
                column: "ConstellationId");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_Name",
                table: "Planets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planets_PlanetTypeId",
                table: "Planets",
                column: "PlanetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_RegionId",
                table: "Planets",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_SolarSystemId",
                table: "Planets",
                column: "SolarSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanetTypes_Name",
                table: "PlanetTypesCsvFilePath",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegionJumps_ToRegionId",
                table: "RegionJumps",
                column: "ToRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Richnesses_Name",
                table: "Richnesses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystemJumps_FromConstellationId",
                table: "SolarSystemJumps",
                column: "FromConstellationId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystemJumps_FromRegionId",
                table: "SolarSystemJumps",
                column: "FromRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystemJumps_ToConstellationId",
                table: "SolarSystemJumps",
                column: "ToConstellationId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystemJumps_ToRegionId",
                table: "SolarSystemJumps",
                column: "ToRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystemJumps_ToSolarSystemId",
                table: "SolarSystemJumps",
                column: "ToSolarSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystems_ConstellationId",
                table: "SolarSystems",
                column: "ConstellationId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystems_Name",
                table: "SolarSystems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystems_RegionId",
                table: "SolarSystems",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstellationJumps");

            migrationBuilder.DropTable(
                name: "PlanetResources");

            migrationBuilder.DropTable(
                name: "RegionJumps");

            migrationBuilder.DropTable(
                name: "SolarSystemJumps");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropTable(
                name: "Richnesses");

            migrationBuilder.DropTable(
                name: "PlanetTypesCsvFilePath");

            migrationBuilder.DropTable(
                name: "SolarSystems");

            migrationBuilder.DropTable(
                name: "Constellations");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
