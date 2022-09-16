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
	var results =
		from album in Albums
		join artist in Artists on album.ArtistId equals artist.ArtistId
		select new
		{
			Title = album.Title,
			ArtistName = artist.Name,
			ReleaseDate = album.ReleaseYear
		};
		
		results.Dump();

}

// You can define other methods, fields, classes and namespaces here