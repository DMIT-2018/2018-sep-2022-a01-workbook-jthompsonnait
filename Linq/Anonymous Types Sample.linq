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

//  Uaing Navigational Properties & Anonymous data set (Collections)

//  Find all albums released in the 90's (1990 -1999)
//  Order the albums by ascending years and then
//  alphabetically by album title.
//  Display Year, Artist Name and Release Label.

//  Concerns:	a)  not all properties of the Album are to be displayed
//				b)	The order of the properties are to be displayed
//					in a different sequebce than the definition of the
//					properties on the entiry.
//				c)  The artist name is not on the table but on the
//					artist table.

//  Solution:	Use an anonymouse data set.
//	The anonymouse instance is defined within the select by
//	the declared fields (properties)
//  The order of the fields in the instance is defined during the
//  specification of your code.  (How you list them is how they will
//	be display).

//  Method Syntax
Albums
	.Where(x => x.ReleaseYear >= 1990 && x.ReleaseYear < 2000)
	.OrderBy(x => x.ReleaseYear)
	.ThenByDescending(x => x.Title)
	.Select(x => new 
	{
		Year = x.ReleaseYear,
		Title = x.Title,
		Artist = x.Artist.Name,
		Label = x.ReleaseLabel
	}
	).Dump();

//  Sorting on the collection
Albums
	.Where(x => x.ReleaseYear >= 1990 && x.ReleaseYear < 2000)
	.Select(x => new
	{
		Year = x.ReleaseYear,
		Title = x.Title,
		Artist = x.Artist.Name,
		Label = x.ReleaseLabel
	}
	).OrderBy(x => x.Year)
	.ThenByDescending(x => x.Title)
	.Dump();