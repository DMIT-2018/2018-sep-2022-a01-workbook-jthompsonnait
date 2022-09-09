<Query Kind="Expression">
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

//  Our code is using C# grammar/syntax

//  Comments are done using the slash
//  Hot key to comment ctrl+k/ctrl+c
//  Uncomments use ctrl+k/ctrl+u
//  Alternatively use ctrl+/ as a toggle

//  Expression IDE
//  Single Linq query statements without a semi-colon
//  You can have multiple statements in your file but
//  if you do so, you MUST highlight the statement to execute.

//  Executing:  Use F5 or the green triangle on the query menu.
//  If the query seem to be not ending, you can use the red square to 
//  terminate the query.



//  Query Syntax
//  Uses a "SQL-like" syntax
//  View the stuendt notes for more examples
from x in Albums
where x.ReleaseYear == 2000
select x

//  Method Syntax
//  Uses C# method syntax (OOP language grammar)
//  To excute a method on a collection, you need to use the
//  access operator (dot operator)  ie: .Where(
//  This reuslts in the returning of an other collection from the method line
//  Method name starts with a captial
//  Method contains contents as a delegate.
//  Delegate describe the action to be done.
Albums
	.Where(x => x.ReleaseYear == 2000)
	.Select(x => x)