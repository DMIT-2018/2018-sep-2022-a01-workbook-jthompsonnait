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

//	Use non adjacent tables in a query
//  Using multiple reporting tables
//	TableA -> TableB -> TableC (grandparent -> parent -> child)
//  Report from TableA and TableC but not from TableB
//  Employee -> Skills but not use the nav properity of EmploymentSkill

//	Report from Albums and PlayListTracks but not from Tracks

//  One possible way of doing the query is to physically join the involved tables
//	 using the Join cLause.
//	HOWEVER:	This limits and confines the optimization that of Linq and SQL can
//				 create.  It works BUT you should FIRST ALWAYS consider using
//				 navigational properties BEFOR doing you own join conditions.

//	List all albums (Title) o fthe 70's with the number of songs that exists
//	 on the album (aggregate).  Also, list the PlayListName and the owner of the
//	 playlist and the song.
//	Album -> Tracks   -> PlayListTracks
//  Aggregate is Albums.Count(Track)
//	PlayListTracks can extract info from parents:  Track and Playlist

//	Method and query (muliple from clause) syntax

Albums
	.Where(x => x.ReleaseYear >= 1970 && x.ReleaseYear < 1980)
	.Select( x => new
	{
		Title = x.Title,
		TrackCount = x.Tracks.Count(),
		PlayListSongs = from tr in x.Tracks
						from plTr in tr.PlaylistTracks
						select new
						{
							Song = plTr.Track.Name,
							Playlist = plTr.Playlist.Name,
							ListOwner = plTr.Playlist.UserName
						}
	})
	.OrderBy(x => x.Title)
	.Dump()
	;
	
	
	
	
	
	
	
	
	
	