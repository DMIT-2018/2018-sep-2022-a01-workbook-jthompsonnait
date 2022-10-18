<Query Kind="Program">
  <Connection>
    <ID>f82afac0-3050-40c9-b69d-e7aa1885402e</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.</Server>
    <Database>Chinook2018</Database>
    <DisplayName>Chinook-Entity</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	//  Post

	//  query method using linq to entity
	//  Track_FetchTrackBy
	//  Track is our BLL class name
	//	FetchTrackBy is our method name

	string searchPattern = "deep";
	string searchType = "Artist";

	List<TrackSelection> trackList_display = Track_FetchTrackBy(searchType, searchPattern);

}

// You can define other methods, fields, classes and namespaces here

public class TrackSelection
{
	public int TrackId { get; set; }
	public string SongName { get; set; }
	public string AlbumTitle { get; set; }
	public string ArtistName { get; set; }
	public int Milliseconds { get; set; }
	public decimal Price { get; set; }
}

#region Track Class
#region Query
public List<TrackSelection> Track_FetchTrackBy(string searchType,
												string searchPattern)
{
	IEnumerable<TrackSelection> tracks = Tracks
	.Where(x => searchType.Equals("Artist") ?
								x.Album.Artist.Name.Contains(searchPattern) :
								x.Album.Title.Contains(searchPattern))
				//.Where(x => (x.Album.Artist.Name.Contains(searchPattern)
				//				&& searchType.Equals("Artist"))
				//			|| (x.Album.Title.Contains(searchPattern)
				//				&& searchType.Equals("Album")))
				.Select(x => new TrackSelection
				{
					TrackId = x.TrackId,
					SongName = x.Name,
					AlbumTitle = x.Album.Title,
					ArtistName = x.Album.Artist.Name,
					Milliseconds = x.Milliseconds,
					Price = x.UnitPrice
				})
	.Dump()
	;

	return tracks.ToList();



}

#endregion
#endregion







