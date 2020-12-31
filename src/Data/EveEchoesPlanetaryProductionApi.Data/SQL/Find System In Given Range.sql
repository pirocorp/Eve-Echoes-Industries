--Get All Neighbours
     SELECT System2.Id, System2.Name
       FROM [graph].[Systems] AS System1, 
	        [graph].[Jumps] AS Jumps,
			[graph].[Systems] AS System2
WHERE MATCH (System1-(Jumps)->System2)
		AND System1.[Name] = 'KDV-DE';


--Get All Neighbours within given range
     SELECT *,
            STRING_AGG([System2].[Name], '->') WITHIN GROUP(GRAPH PATH) AS Destinations
	   FROM [graph].[Systems] AS System1,
		    [graph].[Jumps] FOR PATH AS jump,
		    [graph].[Systems] FOR PATH AS System2
WHERE MATCH (SHORTEST_PATH(System1(-(jump)->System2){1,5}))
        AND System1.name = 'Jita';


--Get All Neighbours within given distance
     SELECT 
            STRING_AGG(DestinationSystem.[Name], '->') WITHIN GROUP(GRAPH PATH) AS Jumps
	   FROM [graph].[Systems] AS SourceSystem,
		    [graph].[Jumps] FOR PATH AS Jump,
		    [graph].[Systems] FOR PATH AS DestinationSystem
WHERE MATCH (SHORTEST_PATH(SourceSystem(-(Jump)->DestinationSystem){1,5}))
        AND SourceSystem.[Name] = 'KDV-DE';


--Find Shortest path
SELECT SourceSystemName, Jumps
FROM (	
	SELECT
		SourceSystem.[Name] AS SourceSystemName, 
		STRING_AGG(DestinationSystem.[Name], '->') WITHIN GROUP (GRAPH PATH) AS Jumps,
		LAST_VALUE(DestinationSystem.[Name]) WITHIN GROUP (GRAPH PATH) AS LastNode
	FROM
		[graph].[Systems] AS SourceSystem,
		[graph].[Jumps] FOR PATH AS Jump,
		[graph].[Systems] FOR PATH  AS DestinationSystem
	WHERE MATCH(SHORTEST_PATH(SourceSystem(-(Jump)->DestinationSystem)+))
	AND SourceSystem.[Name] = 'Jita'
) AS [Path]
WHERE [Path].LastNode = 'KDV-DE';