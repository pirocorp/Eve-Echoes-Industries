﻿namespace EveEchoesPlanetaryProductionApi.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class StoredProceduresForQueryingGraph : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // --EXECUTE [graph].[GetAllNeighbours] N'KDV-DE';
            var getAllNeighbours = @"
                 DROP PROCEDURE IF EXISTS [graph].[GetAllNeighbours]
			                           GO

                        CREATE PROCEDURE [graph].[GetAllNeighbours]  
                            @SystemName nvarchar(100)
                        AS   
                             SELECT DestinationSystem.[Id], DestinationSystem.[Name]
                               FROM [graph].[Systems] AS SourceSystem, 
	                                [graph].[Jumps] AS Jumps,
			                        [graph].[Systems] AS DestinationSystem
                        WHERE MATCH (SourceSystem-(Jumps)->DestinationSystem)
		                        AND SourceSystem.[Name] = @SystemName;
                                 GO";

            migrationBuilder.Sql(getAllNeighbours);

            //--EXECUTE [graph].[AllNeighboursWithinGivenDistance] N'KDV-DE', N'5';
            var getAllNeighboursWithinGivenDistance = @"
                DROP PROCEDURE IF EXISTS [graph].[AllNeighboursWithinGivenDistance]
					                  GO

                CREATE PROCEDURE [graph].[AllNeighboursWithinGivenDistance]
                    @SystemName nvarchar(100),
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
			                AND SourceSystem.[Name] = @SystemName;'

                   EXEC sp_executesql @sql, N'@SystemName nvarchar(100), @distance nvarchar(4)', @SystemName, @distance 
	                 GO ";

            migrationBuilder.Sql(getAllNeighboursWithinGivenDistance);

            //--EXECUTE [graph].[FindShortestPath] N'Jita', N'KDV-DE';
            var findShortestPathBetweenTwoSystems = @"
                DROP PROCEDURE IF EXISTS [graph].[FindShortestPath]
			                   GO

                CREATE PROCEDURE [graph].[FindShortestPath]
                    @SourceSystem nvarchar(100),
	                @DestinationSystem nvarchar(100)	
                AS
	                  SELECT SourceSystemName, Jumps
		                FROM (	 SELECT
						                SourceSystem.[Name] AS SourceSystemName, 
						                STRING_AGG(DestinationSystem.[Name], '->') WITHIN GROUP (GRAPH PATH) AS Jumps,
						                LAST_VALUE(DestinationSystem.[Name]) WITHIN GROUP (GRAPH PATH) AS LastNode
				                   FROM
						                [graph].[Systems] AS SourceSystem,
						                [graph].[Jumps] FOR PATH AS Jump,
						                [graph].[Systems] FOR PATH  AS DestinationSystem
			                WHERE MATCH (SHORTEST_PATH(SourceSystem(-(Jump)->DestinationSystem)+))
			                        AND SourceSystem.[Name] = @SourceSystem
			                ) AS [Path]
	                  WHERE [Path].LastNode = @DestinationSystem;
		                 GO";

            migrationBuilder.Sql(findShortestPathBetweenTwoSystems);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"
                DROP PROCEDURE IF EXISTS [graph].[GetAllNeighbours]
			                          GO

                DROP PROCEDURE IF EXISTS [graph].[AllNeighboursWithinGivenDistance]
					                  GO
                
                DROP PROCEDURE IF EXISTS [graph].[FindShortestPath]
			                          GO";

            migrationBuilder.Sql(sql);
        }
    }
}
