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

//  Statement IDE

// You can have multiple queries written in this IDE environment.
//  You can execute a query individually by highlighting the desired
//    query first.
//  BY DEFAULT executing the file in this enviroment will execute
//  ALL queries from top to bottom

//  IMPORTANT:  Query Syntax
//  Queries in this environment MUST be written using
//  C# language grammare for statements.  This mean that 
//  each statements must end in a semi-colon.
//  Results:  Must be placed in a receiving variable.
//  To display the results, use the LinqPad method .Dump();

//  It appears that Method Syntax does NOT need a semi-colon
//  on the query.
//  However it does need the .DUMP() method to display results
//  (Bug LinqPad 7.0)

//  Find all albums release in 2000.
//  Display the entire album records
//  Query
int years = 1990;
var albums = (from x in Albums
where x.ReleaseYear == years
select x);
albums.Dump();

//  Method
years = 2000;
Albums
	.Where(x => x.ReleaseYear == years)
	.Select(x => x).Dump();
