using Microsoft.EntityFrameworkCore.Migrations;

namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    public partial class RenameToPluralMappingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstellationJumps_Constellations_FromConstellationId",
                table: "ConstellationJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstellationJumps_Constellations_ToConstellationId",
                table: "ConstellationJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstellationJumps_Regions_FromRegionId",
                table: "ConstellationJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstellationJumps_Regions_ToRegionId",
                table: "ConstellationJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_RegionJumps_Regions_FromRegionId",
                table: "RegionJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_RegionJumps_Regions_ToRegionId",
                table: "RegionJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemJumps_Constellations_FromConstellationId",
                table: "SolarSystemJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemJumps_Constellations_ToConstellationId",
                table: "SolarSystemJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemJumps_Regions_FromRegionId",
                table: "SolarSystemJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemJumps_Regions_ToRegionId",
                table: "SolarSystemJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemJumps_SolarSystems_FromSolarSystemId",
                table: "SolarSystemJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemJumps_SolarSystems_ToSolarSystemId",
                table: "SolarSystemJumps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolarSystemJumps",
                table: "SolarSystemJumps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegionJumps",
                table: "RegionJumps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConstellationJumps",
                table: "ConstellationJumps");

            migrationBuilder.RenameTable(
                name: "SolarSystemJumps",
                newName: "SolarSystemsJumps");

            migrationBuilder.RenameTable(
                name: "RegionJumps",
                newName: "RegionsJumps");

            migrationBuilder.RenameTable(
                name: "ConstellationJumps",
                newName: "ConstellationsJumps");

            migrationBuilder.RenameIndex(
                name: "IX_SolarSystemJumps_ToSolarSystemId",
                table: "SolarSystemsJumps",
                newName: "IX_SolarSystemsJumps_ToSolarSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_SolarSystemJumps_ToRegionId",
                table: "SolarSystemsJumps",
                newName: "IX_SolarSystemsJumps_ToRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_SolarSystemJumps_ToConstellationId",
                table: "SolarSystemsJumps",
                newName: "IX_SolarSystemsJumps_ToConstellationId");

            migrationBuilder.RenameIndex(
                name: "IX_SolarSystemJumps_FromRegionId",
                table: "SolarSystemsJumps",
                newName: "IX_SolarSystemsJumps_FromRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_SolarSystemJumps_FromConstellationId",
                table: "SolarSystemsJumps",
                newName: "IX_SolarSystemsJumps_FromConstellationId");

            migrationBuilder.RenameIndex(
                name: "IX_RegionJumps_ToRegionId",
                table: "RegionsJumps",
                newName: "IX_RegionsJumps_ToRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_ConstellationJumps_ToRegionId",
                table: "ConstellationsJumps",
                newName: "IX_ConstellationsJumps_ToRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_ConstellationJumps_ToConstellationId",
                table: "ConstellationsJumps",
                newName: "IX_ConstellationsJumps_ToConstellationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConstellationJumps_FromRegionId",
                table: "ConstellationsJumps",
                newName: "IX_ConstellationsJumps_FromRegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolarSystemsJumps",
                table: "SolarSystemsJumps",
                columns: new[] { "FromSolarSystemId", "ToSolarSystemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegionsJumps",
                table: "RegionsJumps",
                columns: new[] { "FromRegionId", "ToRegionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConstellationsJumps",
                table: "ConstellationsJumps",
                columns: new[] { "FromConstellationId", "ToConstellationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ConstellationsJumps_Constellations_FromConstellationId",
                table: "ConstellationsJumps",
                column: "FromConstellationId",
                principalTable: "Constellations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstellationsJumps_Constellations_ToConstellationId",
                table: "ConstellationsJumps",
                column: "ToConstellationId",
                principalTable: "Constellations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstellationsJumps_Regions_FromRegionId",
                table: "ConstellationsJumps",
                column: "FromRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstellationsJumps_Regions_ToRegionId",
                table: "ConstellationsJumps",
                column: "ToRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionsJumps_Regions_FromRegionId",
                table: "RegionsJumps",
                column: "FromRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionsJumps_Regions_ToRegionId",
                table: "RegionsJumps",
                column: "ToRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemsJumps_Constellations_FromConstellationId",
                table: "SolarSystemsJumps",
                column: "FromConstellationId",
                principalTable: "Constellations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemsJumps_Constellations_ToConstellationId",
                table: "SolarSystemsJumps",
                column: "ToConstellationId",
                principalTable: "Constellations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemsJumps_Regions_FromRegionId",
                table: "SolarSystemsJumps",
                column: "FromRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemsJumps_Regions_ToRegionId",
                table: "SolarSystemsJumps",
                column: "ToRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemsJumps_SolarSystems_FromSolarSystemId",
                table: "SolarSystemsJumps",
                column: "FromSolarSystemId",
                principalTable: "SolarSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemsJumps_SolarSystems_ToSolarSystemId",
                table: "SolarSystemsJumps",
                column: "ToSolarSystemId",
                principalTable: "SolarSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstellationsJumps_Constellations_FromConstellationId",
                table: "ConstellationsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstellationsJumps_Constellations_ToConstellationId",
                table: "ConstellationsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstellationsJumps_Regions_FromRegionId",
                table: "ConstellationsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstellationsJumps_Regions_ToRegionId",
                table: "ConstellationsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_RegionsJumps_Regions_FromRegionId",
                table: "RegionsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_RegionsJumps_Regions_ToRegionId",
                table: "RegionsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemsJumps_Constellations_FromConstellationId",
                table: "SolarSystemsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemsJumps_Constellations_ToConstellationId",
                table: "SolarSystemsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemsJumps_Regions_FromRegionId",
                table: "SolarSystemsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemsJumps_Regions_ToRegionId",
                table: "SolarSystemsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemsJumps_SolarSystems_FromSolarSystemId",
                table: "SolarSystemsJumps");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystemsJumps_SolarSystems_ToSolarSystemId",
                table: "SolarSystemsJumps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolarSystemsJumps",
                table: "SolarSystemsJumps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegionsJumps",
                table: "RegionsJumps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConstellationsJumps",
                table: "ConstellationsJumps");

            migrationBuilder.RenameTable(
                name: "SolarSystemsJumps",
                newName: "SolarSystemJumps");

            migrationBuilder.RenameTable(
                name: "RegionsJumps",
                newName: "RegionJumps");

            migrationBuilder.RenameTable(
                name: "ConstellationsJumps",
                newName: "ConstellationJumps");

            migrationBuilder.RenameIndex(
                name: "IX_SolarSystemsJumps_ToSolarSystemId",
                table: "SolarSystemJumps",
                newName: "IX_SolarSystemJumps_ToSolarSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_SolarSystemsJumps_ToRegionId",
                table: "SolarSystemJumps",
                newName: "IX_SolarSystemJumps_ToRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_SolarSystemsJumps_ToConstellationId",
                table: "SolarSystemJumps",
                newName: "IX_SolarSystemJumps_ToConstellationId");

            migrationBuilder.RenameIndex(
                name: "IX_SolarSystemsJumps_FromRegionId",
                table: "SolarSystemJumps",
                newName: "IX_SolarSystemJumps_FromRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_SolarSystemsJumps_FromConstellationId",
                table: "SolarSystemJumps",
                newName: "IX_SolarSystemJumps_FromConstellationId");

            migrationBuilder.RenameIndex(
                name: "IX_RegionsJumps_ToRegionId",
                table: "RegionJumps",
                newName: "IX_RegionJumps_ToRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_ConstellationsJumps_ToRegionId",
                table: "ConstellationJumps",
                newName: "IX_ConstellationJumps_ToRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_ConstellationsJumps_ToConstellationId",
                table: "ConstellationJumps",
                newName: "IX_ConstellationJumps_ToConstellationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConstellationsJumps_FromRegionId",
                table: "ConstellationJumps",
                newName: "IX_ConstellationJumps_FromRegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolarSystemJumps",
                table: "SolarSystemJumps",
                columns: new[] { "FromSolarSystemId", "ToSolarSystemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegionJumps",
                table: "RegionJumps",
                columns: new[] { "FromRegionId", "ToRegionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConstellationJumps",
                table: "ConstellationJumps",
                columns: new[] { "FromConstellationId", "ToConstellationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ConstellationJumps_Constellations_FromConstellationId",
                table: "ConstellationJumps",
                column: "FromConstellationId",
                principalTable: "Constellations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstellationJumps_Constellations_ToConstellationId",
                table: "ConstellationJumps",
                column: "ToConstellationId",
                principalTable: "Constellations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstellationJumps_Regions_FromRegionId",
                table: "ConstellationJumps",
                column: "FromRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstellationJumps_Regions_ToRegionId",
                table: "ConstellationJumps",
                column: "ToRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionJumps_Regions_FromRegionId",
                table: "RegionJumps",
                column: "FromRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionJumps_Regions_ToRegionId",
                table: "RegionJumps",
                column: "ToRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemJumps_Constellations_FromConstellationId",
                table: "SolarSystemJumps",
                column: "FromConstellationId",
                principalTable: "Constellations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemJumps_Constellations_ToConstellationId",
                table: "SolarSystemJumps",
                column: "ToConstellationId",
                principalTable: "Constellations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemJumps_Regions_FromRegionId",
                table: "SolarSystemJumps",
                column: "FromRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemJumps_Regions_ToRegionId",
                table: "SolarSystemJumps",
                column: "ToRegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemJumps_SolarSystems_FromSolarSystemId",
                table: "SolarSystemJumps",
                column: "FromSolarSystemId",
                principalTable: "SolarSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystemJumps_SolarSystems_ToSolarSystemId",
                table: "SolarSystemJumps",
                column: "ToSolarSystemId",
                principalTable: "SolarSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
