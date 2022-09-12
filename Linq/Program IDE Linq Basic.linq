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
	//  Program IDE
	//  You can have multiple queries written in the IDE environment
	//  This environment works "like" a console application

	//  This allows one to pre-test complete components that can
	//  be moved directly into your backend application
	//  (class library)

	//  IMPORTANT:  Query Syntax
	//  Queries in this environment MUST be written using
	//  C# language grammare for statements.  This mean that 
	//  each statements must end in a semi-colon.
	//  Results:  Must be placed in a receiving variable.
	//  To display the results, use the LinqPad method .Dump();

	// query
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
		
	//  Image this is a method in your BLL server
	
	List<Albums> GetAllAlbumsQ(int paramYear)
	{
		var resultsQ = from x in Albums
		where x.ReleaseYear == paramYear
		select x;
		return resultsQ.ToList();
	}
	
	List<Albums> resultQ = GetAllAlbumsQ(2000);
	resultQ.Dump();
}

