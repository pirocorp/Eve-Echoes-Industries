namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddFullTextIndexToSolarSystemName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;",
                suppressTransaction: true);

            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT INDEX ON SolarSystems(Name) KEY INDEX PK_SolarSystems;",
                suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(sql: "DROP FULLTEXT INDEX ON SolarSystems(Name)");

            migrationBuilder.Sql(sql: "DROP FULLTEXT CATALOG ftCatalog");
        }
    }
}
