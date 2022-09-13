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

//  Sorting

//  There is a significant diffence between query and method
//  syntax.

//  Query syntax is much like SQL
//     orderby field {[ascending (default)] | descending}
//             , field {[ascending (default)]}

//  ascending is the default.

//  Method syntax is a series of individual methods.
//  Start up
//  .OrderBy(x => x.field)  		  //  ascending
//  .OrderByDescending(x => x.field)  //  descending
//  After on of these two beginning methods, if you have any
//  other field(s)
//  .ThenBy(x => x.field)             //  ascending
//  .ThenByDescending(x => x.field)   //  descending

//  Find all albums released in the 90's (1990 -1999)
//  Order the albums by ascending years and then
//  alphabetically by album title.
//  Display the entire album record.

//  If not clear specification on ascending or descending,
//  assume ascending.
//  Often the ordering phrase may be done with the word
//  "Within".
//  Without the "within", the implied order of your fields
//  are major to minor.
//  With the "within", the implied order is minor to major
//  for your list of fields.
//  (order alphabetically by album title within years)

//  Query synatx
(from x in Albums
orderby x.ReleaseYear, x.Title descending
where x.ReleaseYear >= 1990 && x.ReleaseYear < 2000
select x).Dump();

//  Method Syntax
Albums
	.Where(x => x.ReleaseYear >= 1990 && x.ReleaseYear < 2000)
	.OrderBy(x => x.ReleaseYear)
	.ThenByDescending(x => x.Title)
	.Select(x => x).Dump();
	