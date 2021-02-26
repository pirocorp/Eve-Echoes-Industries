namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class GraphWithNodesSystemsFromSolarSystemsAndEdgesJumpsFromSolarSystemsJumps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
                DROP SCHEMA IF EXISTS [graph]
                                   GO

                CREATE SCHEMA [graph];
                GO

                DROP TABLE IF EXISTS [graph].[Systems];
                                  GO

                CREATE TABLE [graph].[Systems] (
	                [Id] INT PRIMARY KEY,
	                [Security] FLOAT NOT NULL,
	                [RegionId] BIGINT NOT NULL,
	                [ConstellationId] BIGINT NOT NULL,
	                [Name] NVARCHAR(100) NOT NULL
                ) AS NODE;
                GO

                INSERT INTO [graph].[Systems]([Id], [Security], [RegionId], [ConstellationId], [Name])
	                 SELECT [System].[Id], 
			                [System].[Security],
			                [System].[RegionId],
			                [System].[ConstellationId],
			                [System].[Name]
	                   FROM [dbo].[SolarSystems] AS [System];
		                 GO


                DROP TABLE IF EXISTS [graph].[Jumps];
		                   GO

                CREATE TABLE [graph].[Jumps] AS EDGE;
		                  GO

                INSERT INTO [graph].[Jumps]($from_id, $to_id)
	                 SELECT s.Node1, d.Node2
	                   FROM [dbo].[SolarSystemsJumps] AS [j]
                 INNER JOIN (SELECT $node_id AS node1, Id FROM [graph].[Systems]) s
                         ON [j].[FromSolarSystemId] = s.Id
                 INNER JOIN (SELECT $node_id AS node2, Id FROM [graph].[Systems]) d
                         ON [j].[ToSolarSystemId] = d.Id
		                 GO
            ";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"
                DROP TABLE IF EXISTS [graph].[Jumps];
		                          GO

                DROP TABLE IF EXISTS [graph].[Systems];
                                  GO

               DROP SCHEMA IF EXISTS [graph]
                                 GO";

            migrationBuilder.Sql(sql);
        }
    }
}
