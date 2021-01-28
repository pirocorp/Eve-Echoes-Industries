namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RenameColumnProductionCountInTableBlueprint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductonCount",
                table: "Blueprints",
                newName: "ProductionCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionCount",
                table: "Blueprints",
                newName: "ProductonCount");
        }
    }
}
