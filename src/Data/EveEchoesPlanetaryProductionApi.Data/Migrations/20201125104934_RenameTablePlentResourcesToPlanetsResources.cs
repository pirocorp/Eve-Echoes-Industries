namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RenameTablePlentResourcesToPlanetsResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanetResources_Items_ItemId",
                table: "PlanetResources");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanetResources_Planets_PlanetId",
                table: "PlanetResources");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanetResources_Richnesses_RichnessId",
                table: "PlanetResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanetResources",
                table: "PlanetResources");

            migrationBuilder.RenameTable(
                name: "PlanetResources",
                newName: "PlanetsResources");

            migrationBuilder.RenameIndex(
                name: "IX_PlanetResources_RichnessId",
                table: "PlanetsResources",
                newName: "IX_PlanetsResources_RichnessId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanetResources_ItemId",
                table: "PlanetsResources",
                newName: "IX_PlanetsResources_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanetsResources",
                table: "PlanetsResources",
                columns: new[] { "PlanetId", "ItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlanetsResources_Items_ItemId",
                table: "PlanetsResources",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanetsResources_Planets_PlanetId",
                table: "PlanetsResources",
                column: "PlanetId",
                principalTable: "Planets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanetsResources_Richnesses_RichnessId",
                table: "PlanetsResources",
                column: "RichnessId",
                principalTable: "Richnesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanetsResources_Items_ItemId",
                table: "PlanetsResources");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanetsResources_Planets_PlanetId",
                table: "PlanetsResources");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanetsResources_Richnesses_RichnessId",
                table: "PlanetsResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanetsResources",
                table: "PlanetsResources");

            migrationBuilder.RenameTable(
                name: "PlanetsResources",
                newName: "PlanetResources");

            migrationBuilder.RenameIndex(
                name: "IX_PlanetsResources_RichnessId",
                table: "PlanetResources",
                newName: "IX_PlanetResources_RichnessId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanetsResources_ItemId",
                table: "PlanetResources",
                newName: "IX_PlanetResources_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanetResources",
                table: "PlanetResources",
                columns: new[] { "PlanetId", "ItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlanetResources_Items_ItemId",
                table: "PlanetResources",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanetResources_Planets_PlanetId",
                table: "PlanetResources",
                column: "PlanetId",
                principalTable: "Planets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanetResources_Richnesses_RichnessId",
                table: "PlanetResources",
                column: "RichnessId",
                principalTable: "Richnesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
