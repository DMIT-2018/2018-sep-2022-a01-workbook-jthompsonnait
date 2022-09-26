<Query Kind="Statements">
  <Connection>
    <ID>8f6c621e-f256-443a-94b4-98581ddcde18</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook2018</Database>
  </Connection>
</Query>

//	Unions()
//  Since Linq is converted into SQL, one would expect that the SQL UNion rule must be the same in LINQ
//	Purpose:	Concatenating multiple results into one collection
//	Syntax:		(queryA).Union(queryB)[.Union(query...)]
//	Rules:
//		a)	number of columns are the same
//		b)	column datatypes must match
//		c)	ordering should be done as a method after the last union

//	List the stats (count, cost, average track length) if albums of tracks
//	NOTE:	For cost and average, one will need an instance in tracks to do the aggregation.
//	Concern:	What if the album does not have any recorded tracks.
//				Albums with no tracks on the database will have a count, however
//				 cost and average length will be 0 (no instances to aggreagte)
//	Solution:	Create two queries, one handling no tracks and one handling albums with tracks
//				 then union the two results
//	NOTE:		If you hard coding numeric fields, the query with the hard code values MUST be the first query.
//	queryA would be the albums with no tracks (hard code cost and average)
//	queryB would be the albums with tracks (cost and average will be calculated)

Albums
	.Where(x => x.Tracks.Count() == 0)
	.Select(x => new
	{
		Title = x.Title,
		TotalTracks = x.Tracks.Count(),
		TotalCost = 0.00m,
		AverageLength = 0.00
	})
.Union(
Albums
.Where(x => x.Tracks.Count() > 0)
.Select(x => new
{
	Title = x.Title,
	TotalTracks = x.Tracks.Count(),
	TotalCost = x.Tracks.Sum(t => t.UnitPrice),
	AverageLength = x.Tracks.Average(t => t.Milliseconds /1000)
})).OrderBy(x=> x.TotalTracks)
.Dump()
;











