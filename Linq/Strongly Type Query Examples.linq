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
	//  Strongly type quesy dataset

	//  Anonymous dataset from a query does NOT have a permanent specified class
	//   definition (dynamic)
	//  Strongly type query dataset HAS a permanent class definition in its code.

	//  Image your solutions needs to mimic a web app query to some BLL service

	string partialSongName = "dance";
	List<Song> result = SongByPartialName(partialSongName);
	result.Dump();  //  mimic your table display on our web page.

}

//  You can...
//  Developer defined data type,

//  Find all songs that contains a partial string of a track name.
//  Display the ALbum, Song (track name), Artist.



public class Song
{
	public string AlbumTitle { get; set; }
	public string SongTitle { get; set; }
	public string Artist { get; set; }
}

List<Song> SongByPartialName(string partialSongName)
{
	var songCollection = Tracks
							.Where(x => x.Name.Contains(partialSongName))
							.Select(x => new Song
							{
								AlbumTitle = x.Album.Title,
								SongTitle = x.Name,
								Artist = x.Album.Artist.Name
							});
	return songCollection.ToList();

}