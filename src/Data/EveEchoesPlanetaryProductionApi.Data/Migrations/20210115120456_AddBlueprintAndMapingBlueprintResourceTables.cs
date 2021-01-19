namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddBlueprintAndMapingBlueprintResourceTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blueprints",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlueprintItemId = table.Column<long>(type: "bigint", nullable: false),
                    ProductTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    TechLevel = table.Column<int>(type: "int", nullable: false),
                    ProductionCost = table.Column<long>(type: "bigint", nullable: false),
                    ProductionTime = table.Column<long>(type: "bigint", nullable: false),
                    ProductonCount = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blueprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blueprints_Items_BlueprintItemId",
                        column: x => x.BlueprintItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blueprints_Items_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blueprints_ItemTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlueprintsResources",
                columns: table => new
                {
                    BlueprintId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlueprintsResources", x => new { x.BlueprintId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_BlueprintsResources_Blueprints_BlueprintId",
                        column: x => x.BlueprintId,
                        principalTable: "Blueprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlueprintsResources_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blueprints_BlueprintItemId",
                table: "Blueprints",
                column: "BlueprintItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blueprints_ProductId",
                table: "Blueprints",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blueprints_ProductTypeId",
                table: "Blueprints",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BlueprintsResources_ItemId",
                table: "BlueprintsResources",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlueprintsResources");

            migrationBuilder.DropTable(
                name: "Blueprints");
        }
    }
}
