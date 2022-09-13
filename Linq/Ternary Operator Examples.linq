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

//  Using the Ternary Operator

//  Conditions(s)  ? True Value : False Value

//  Both the true value and false value MUST resolve to a SINGLE 
//  piece of data (a single value)
//  Compare to the conditioan statement (if statement)

//  If (condition(s))
//  ? True path (complex logic)
//  : else 
//  false (complex logic)

//  Just like the conditional statement which can be nested logic,
//  the true value and faluse value can have nested ternary operators
//  as long as the final result resolves to a SINGLE value

//  List all albums by release label.  Any album with no label
//  indicated as Unknown.
//  List columns Title, Label, Artist Name and Release Year.

//  Understatnd problem
//  Collection: ALbums
//  Select data set:  anonymous data set.
//  Ordering:  release label
//  Label:  either Unknown or label  ***  something to think about

Albums
	.Select(x => new
	{
		Title = x.Title,
		Label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel,
		Artist = x.Artist.Name,
		Year = x.ReleaseYear,
	}
	)
	.OrderBy(x => x.Label)
	.Dump();

//  HOMEWORK!!!!!!
//  List all albums showing the Title, Artist Name, Year and
//  decade of releases (Oldies, 70s, 80s, 90s or Modern)
//  Order by decade