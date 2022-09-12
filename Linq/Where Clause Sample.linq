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

// Where Clause

// Filter clause.
//  The conditions are setup as you would in C#
//  Beware that LinqPad nay NOT like some C# syntax (DateTime)
//  Beware that Linq is converted to SQL which may not be 
//  Lkike certains C# syntax because it could not be converted.

//  Syntax
//  Query
//  Where condition [logical operator (and/or) condition 2]
//  Method
//  notice that the method syntax make of tthe Lambda expressions
//  .Where(Lambda expression)
//  .Where (x => condition [logical operator (and/or) operator2)


//  Query
//  Find all albums in the 90's (1990 - 1999)
//  Display the entire album records
var albums = (from x in Albums
			  where x.ReleaseYear >= 1990 && x.ReleaseYear < 2000
			  select x);
albums.Dump();

//  Method
Albums
	.Where(x => x.ReleaseYear >= 1990 && x.ReleaseYear < 2000)
	.Select(x => x).Dump();
	
//  Find all of the albums for the artist Queen
//  Concern:  The artist name is in another table in an
//            SQL.  You would use an inner join if you got the results
//            from SQL.
//            In Linq, you DO NOT want to specific your joins unless
//			  absolutely necessary.  Instead use the "navigational
//			  properties" of your entity to generate the relationship.

Albums
	.Where(x => x.Artist.Name.Equals("Queen"))
	.Select(x => x).Dump();

Albums
.Where(x => x.Artist.Name == "Queen")
.Select(x => x).Dump();

//  .Equals() is an exact match of a string.  You could also used
//  the (==).  In SQL "=" or like 'string'
//  .Contains() is a partial string match.  In SQL like '%string%'.
//  For numerics, use your relative operators(==, <, >, >=, !=, etc).

Albums
	.Where(x => x.HasValue())  //  This does not work in LinqPad.
	
Albums
	.Where(x => x.ReleaseLabel == null)
	.Select(x => x).Dump();
	