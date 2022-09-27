<Query Kind="Program">
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

void Main()
{
	//  Conversion  .ToList()

	//Albums.ToList();

	//Albums.Select(a => a).ToList();

	//  Display all albums and their tracks
	//  Display the album title, artist name and album tracks
	//  For each track, show the song name and playtime in seconds
	//  Show only albums with 25 or more tracks

	List<AlbumTrack> albumList = Albums
						.Where(a => a.Tracks.Count() >= 25)
						.Select(a => new AlbumTrack
						{
							Title = a.Title,
							Artist = a.Artist.Name,
							Songs = a.Tracks.Select(tr => new SongItem
							{
								Song = tr.Name,
								PlayTime = tr.Milliseconds / 1000
							}).ToList()
						}).ToList()
						//.Dump()
						;

	//  Typically if the albumList was a var variable in your BLL method
	//   AND the method return datatype was a List<T>, one could on the 
	//	  return statement do: return albumlist.ToList(): (saw in your 1517 course)

	//  Using .FirstOrDefault()
	//  Great for lookup that your expect 0, 1, or more instances return
	//  FInd the first album of Deep Purple

	string artistParam = "Deep Purple";
	var resultFOD = Albums
					.Where(a => a.Artist.Name.Equals(artistParam))
					.Select(a => a)
					.OrderBy(a => a.ReleaseYear)
					.FirstOrDefault()
					//.Dump()
					;

	//if (resultFOD != null)
	//	resultFOD.Dump();
	//else
	//	Console.WriteLine($"No album found for {artistParam}");

	//  Using SingleOrDefault()
	//  Expecting at MOST a single instance being return

	int albumID = 10000;
	var resultSOD = Albums
					.Where(a => a.AlbumId == albumID)
					.Select(a => a)
					.OrderBy(a => a.ReleaseYear)
					.SingleOrDefault()
					//.Dump()
					;

	//if (resultSOD != null)
	//	resultSOD.Dump();
	//else
	//	Console.WriteLine($"No album found for ID  {albumID}");

	// .Distinct()
	//  Removes duplicated reported items

	//  Obtain a list of customer countries
	var resultDinstinct = Customers
					.OrderBy(c => c.Country)
					.Select(c => c.Country)
					.Distinct()
					//.Dump()
					;

	//  .Take() and .SKip()
	//  in 1517, when you wanted to use your paginator
	//   the quesry method ws to return ONLY the needed records to display
	//  a)	You passed in the pagesize and pagenumber
	//	b)	the query was executed, return all rows
	//	c)	Set your out paramater to the .Count() of rows 
	//	d)	Calculated the number of rows to skip(pageNumber -1) * pagesize
	//	e)	On the return statement, against your collection, you used a .Skip and .Take
	//	Return variableName.SKip(rowsSkipped).Take(pagesize).ToList()

	//  Any and All
	//	There are 205 genres on file

	//  Show genres that have tracks which are not on any playlist.
	Genres
		.Where(g => g.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0))
		//.Dump()
		;

	Genres
	.Where(g => g.Tracks.All(tr => tr.PlaylistTracks.Count() > 0))
	//.Dump()
	;

	//  Compare the track collection of 2 people using All and Any
	//	Create a small anonymous collection for 2 people
	//  Roberto Almeida (AlmeidaR) and Michelle Brooks (BrooksM)

	var listA = PlaylistTracks
		.Where(x => x.Playlist.UserName.Equals("AlmeidaR"))
		.Select(x => new
		{
			Song = x.Track.Name,
			Genre = x.Track.Genre.Name,
			id = x.TrackId,
			Artist = x.Track.Album.Artist.Name
		})
		.Distinct()
		.OrderBy(x => x.Song)
		//.Dump()//110
		;

	var listB = PlaylistTracks
	.Where(x => x.Playlist.UserName.Equals("BrooksM"))
	.Select(x => new
	{
		Song = x.Track.Name,
		Genre = x.Track.Genre.Name,
		id = x.TrackId,
		Artist = x.Track.Album.Artist.Name
	})
	.Distinct()
	.OrderBy(x => x.Song)
	//.Dump()//88
	;

	
	
//  List the tracks that BOTH ROberto and MIchelle like
//  COmpare 2 collections together (List A and List B)
//  Assume ListA is Roberta and ListB is Michelle
//  List is the collection you wish to report on.
//  List B is what you wish to compare ListA to (no reporting)

listA
	.Where(lsta => listB.Any(lstb => lstb.id == lsta.id))
	.OrderBy(lsta => lsta.Song)
	//.Dump()
	;

	listB
	.Where(lstb => listA.Any(lsta => lsta.id == lstb.id))
	.OrderBy(lsta => lsta.Song)
	//.Dump()
	;

	//	What songs does Roberta like but not Michelle
	listA
		.Where(lsta => !listB.Any(lstb => lstb.id == lsta.id))
		.OrderBy(lsta => lsta.Song)
	//	.Dump()
		;

	listA
		.Where(lsta => listB.All(lstb => lstb.id != lsta.id))
		.OrderBy(lsta => lsta.Song)
		.Dump()
		;
	//  There maybe time that using a !Any() -> All(!relationship)
//	 and a !All() -> Any(!relationship)

//	Using All and Any in comparing 2 complex collections.
//  If you collection is NOT a complex record (list of integer or string)
//	 there is a Linq method call .Except that can be used to solve your Linq
//    except

}

public class SongItem
{
	public string Song { get; set; }
	public double PlayTime { get; set; }
}

public class AlbumTrack
{
	public string Title { get; set; }
	public string Artist { get; set; }
	public List<SongItem> Songs { get; set; }
}
