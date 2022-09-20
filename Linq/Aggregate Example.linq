<Query Kind="Expression">
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

//  Aggregates
//  .Count()		Count the number of instances in a collection
//	.SUM()			Sums(Total) a numeric field (numeric expression) in the collection
//	.Min()			FInd the minimum value of a collection of values for a field
//	.Max()			FInd the maximum value of a collection of values for a field
//	.Average()		FInd the average value of a collection of values for a field

//  IMPORTANT!!!
//  Aggregates works ONLY on a collection of values for a particular field (expression)
//  Aggregates DO NO work on a single row (not the same as a collection with one row)

//  Syntax
//  Query
//  (from ...
//	...
//	Select expression).aggreage()
//  The expression is resolved to a single field for Sum, Min, Max, Average.

//  Method
//  Collection.aggregate(x => expresssion).Sum, Min, Max and Average
//  NOTE:  Count() does NOT use an expression
//  Collection.Select(x => expression).Aggregate()

//  You can use multiple aggregates on a single column.
//  Collection.Sum(x => expression).Min(x => expression))

//  Find the average playing time (length) of tracks in our music collection.
//  Thought process.
//  Average is an aggregate
//  What is the collection?  A track is a member of the tracks table
//  What the expression?  :  Field Milliseconds representing the track length (playtime)

//  Query
(from x in Tracks
 select x.Milliseconds).Average()

//  Method
//  Using aggregate first
Tracks.Average(x => x.Milliseconds);

//  Using select first
Tracks.Select(x => x.Milliseconds).Average();

//  List all albums of the 60s showing the
//  Album Title, Artist and various aggregates for the albums containing tracks
//  For each album, show numbers of tracks, the longest playing track,
//  the shortest playing track, the total price of all tracks and the
//  average length of the album tracks.
//  Order by years

//	HINT:  Albums has two navigation properties
//		   Artist points to a single parent record.	
//		   Tracks points to the collection of child records (Tracks) of that album.

Albums;

//  Filter by the 60's
Albums
	.Where(x => x.ReleaseYear >= 1960 && x.ReleaseYear < 1970)
	.Select(x => x)
	.Dump();

//  Show the album title and artists
Albums
	.Where(x => x.ReleaseYear >= 1960 && x.ReleaseYear < 1970)
	.Select(x => new
	{
		AlblumTitle = x.Title,
		Artist = x.Artist.Name
	}
	)
	.Dump();

//  Add the track count
Albums
	.Where(x => x.ReleaseYear >= 1960 && x.ReleaseYear < 1970)
	.Select(x => new
	{
		AlblumTitle = x.Title,
		Artist = x.Artist.Name,
		TrackCount = x.Tracks.Count()
	}
	)
	.Dump();

//  Filter by having tracks
Albums
	.Where(x => x.ReleaseYear >= 1960 && x.ReleaseYear < 1970
	&& x.Tracks.Count() > 0)
	.Select(x => new
	{
		AlblumTitle = x.Title,
		Artist = x.Artist.Name,
		TrackCount = x.Tracks.Count()
	}
	)
	.Dump();

//  Find the longest track
Albums
	.Where(x => x.ReleaseYear >= 1960 && x.ReleaseYear < 1970
	&& x.Tracks.Count() > 0)
	.Select(x => new
	{
		AlblumTitle = x.Title,
		Artist = x.Artist.Name,
		TrackCount = x.Tracks.Count(),
		LongestTrack = x.Tracks.Max(t => t.Milliseconds /1000),  //  converts from milliseconds to seconds.
		lt = x.Tracks.Select( tr => tr.Milliseconds /1000).Max()
	}
	)
	.Dump();

//  Find shortest track
Albums
	.Where(x => x.ReleaseYear >= 1960 && x.ReleaseYear < 1970
	&& x.Tracks.Count() > 0)
	.Select(x => new
	{
		AlblumTitle = x.Title,
		Artist = x.Artist.Name,
		TrackCount = x.Tracks.Count(),
		LongestTrack = x.Tracks.Max(t => t.Milliseconds / 1000),  //  converts from milliseconds to seconds.
		lt = x.Tracks.Select(tr => tr.Milliseconds / 1000).Max(),
		ShortestTrack = x.Tracks.Min(t => t.Milliseconds / 1000)
	}
	)
	.Dump();

//  Find total price
Albums
	.Where(x => x.ReleaseYear >= 1960 && x.ReleaseYear < 1970
	&& x.Tracks.Count() > 0)
	.Select(x => new
	{
		AlblumTitle = x.Title,
		Artist = x.Artist.Name,
		TrackCount = x.Tracks.Count(),
		LongestTrack = x.Tracks.Max(t => t.Milliseconds / 1000),  //  converts from milliseconds to seconds.
		lt = x.Tracks.Select(tr => tr.Milliseconds / 1000).Max(),
		ShortestTrack = x.Tracks.Min(t => t.Milliseconds / 1000),
		TotalPrice = x.Tracks.Sum(t => t.UnitPrice)
	}
	)
	.Dump();

//  Average Length
Albums
	.Where(x => x.ReleaseYear >= 1960 && x.ReleaseYear < 1970
	&& x.Tracks.Count() > 0)
	.Select(x => new
	{
		AlblumTitle = x.Title,
		Artist = x.Artist.Name,
		TrackCount = x.Tracks.Count(),
		LongestTrack = x.Tracks.Max(t => t.Milliseconds / 1000),  //  converts from milliseconds to seconds.
		lt = x.Tracks.Select(tr => tr.Milliseconds / 1000).Max(),
		ShortestTrack = x.Tracks.Min(t => t.Milliseconds / 1000),
		TotalPrice = x.Tracks.Sum(t => t.UnitPrice),
		AverageTrack = x.Tracks.Average(t => t.Milliseconds / 1000)
	}
	)
	.Dump();

//  Track Name
Albums
	.Where(x => x.ReleaseYear >= 1960 && x.ReleaseYear < 1970
	&& x.Tracks.Count() > 0)
	.Select(x => new
	{
		AlblumTitle = x.Title,
		Artist = x.Artist.Name,
		TrackCount = x.Tracks.Count(),
		LongestTrack = x.Tracks.Max(t => t.Milliseconds / 1000),  //  converts from milliseconds to seconds.
		LongestName = ().
		lt = x.Tracks.Select(tr => tr.Milliseconds / 1000).Max(),
		ShortestTrack = x.Tracks.Min(t => t.Milliseconds / 1000),
		TotalPrice = x.Tracks.Sum(t => t.UnitPrice),
		AverageTrack = x.Tracks.Average(t => t.Milliseconds / 1000)
	}
	)
	.Dump();

