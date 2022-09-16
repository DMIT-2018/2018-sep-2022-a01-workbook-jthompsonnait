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

var results = Albums.Join(Artists, 
							album => album.ArtistId,
							artist => artist.ArtistId,
							(album, artist) => new
							{
								Title = album.Title,
								ArtistName = artist.Name,
								ReleaseDate = album.ReleaseYear
							});
							
results.Dump();
						
				