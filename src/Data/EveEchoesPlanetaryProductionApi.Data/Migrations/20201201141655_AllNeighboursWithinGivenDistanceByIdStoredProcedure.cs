namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AllNeighboursWithinGivenDistanceByIdStoredProcedure : Migration
    {
        // --EXECUTE [graph].[AllNeighboursWithinGivenDistanceById] N'30001978', N'5';
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var getAllNeighboursWithinGivenDistanceById = @"
                DROP PROCEDURE IF EXISTS [graph].[AllNeighboursWithinGivenDistanceById]
                GO

                CREATE PROCEDURE [graph].[AllNeighboursWithinGivenDistanceById]
                    @systemId bigint,
	                @distance nvarchar(4)	
                AS  
                DECLARE @sql nvarchar(4000)
	                SET @sql = N'
		                 SELECT 
				                STRING_AGG(DestinationSystem.[Id], ''->'') WITHIN GROUP(GRAPH PATH) AS Jumps
		                   FROM [graph].[Systems] AS SourceSystem,
				                [graph].[Jumps] FOR PATH AS Jump,
				                [graph].[Systems] FOR PATH AS DestinationSystem
	                WHERE MATCH (SHORTEST_PATH(SourceSystem(-(Jump)->DestinationSystem){1,' + @distance + '}))
			                AND SourceSystem.[Id] = @systemId;'

	                 exec sp_executesql @sql, N'@systemId nvarchar(100), @distance nvarchar(10)', @systemId, @distance 
                GO ";

            migrationBuilder.Sql(getAllNeighboursWithinGivenDistanceById);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"
                DROP PROCEDURE IF EXISTS [graph].[AllNeighboursWithinGivenDistanceById]
                GO";

            migrationBuilder.Sql(sql);
        }
    }
}
