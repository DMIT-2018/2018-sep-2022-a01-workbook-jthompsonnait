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
						.Where(a => a.Tracks.Count() >=25)
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

//  FInd the first album of Deep Purple

string artistParam = "Deep Purple";
var resultFOD = Albums
				.Where(a => a.Artist.Name.Equals(artistParam))
				.Select(a => a)
				.OrderBy(a => a.ReleaseYear)
				.First()
				.Dump()
				;
}

public class SongItem
	{
		public string Song {get; set;}
		public double PlayTime {get; set;} 
	}
	
	public class AlbumTrack
	{
		public string Title {get; set;}
		public string Artist {get; set;}
		public List<SongItem> Songs {get; set;}
	}
