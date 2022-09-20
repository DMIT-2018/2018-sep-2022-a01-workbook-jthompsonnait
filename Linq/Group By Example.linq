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

//  Grouping

//  When you create a group, it builds two (2) components.
//		a)  Key component (group by criteria value)
//			 reference this compoent using the group name.Key[.Property]
//			(property or column or attribute or value).
//		b)  the data of the group (instance of the original collection) -> mini group or collection

//  ways to group
//		a)  By a single property (colum, field, attribute, vale)  groupname.Key
//		b)	By a set of properties (anonymouse dataset key)  groupname.Key.PropertyName
//		c)	By using an entity (x.navproperty)  **  Try to avoid **

//  Concept Processing.
//  We start with "pile" of data (original collection)
//  Specify the grouping criteria (value(s))
//  Result of the group operation will be to "place the data into smaller piles"
//	  (mini collections).  The piles are dependent on the grouping criteria value(s)
//	The grouping criterial (property(ies), colum, etc) become the key
//	The individual instances are "the data in the smaller piles" (mini collections)
//	The entire individual instances of the original collection is placed in the smaller piles
//	 (mini collections)

//  Manipulation of each of the "smaller piles" is now possible with your Linq commands.

//  Grouping is different than Ordering
//  Ordering is the re-sequencing of a collection for display. (Not manipulation)
//  Grouping re-organizes a collection into seperate, usually smaller collections of processing.

//  Display albums by release years
//	This request does NOT need grouping
//  This request is an re-sequencing (ordering) of output (OrderBy)
//  This affects display only.

Albums
	.OrderBy(x => x.ReleaseYear);
	
//	Display albums grouped by ReleaseYear
//	NOt one display of albums but displays of album for a specified value (ReleaseYear)
//	Explucut request to breakup the display into desired "piles' (mini-collections)

Albums
	.GroupBy(x => x.ReleaseYear);
	
//	Query Syntax
from x in Albums
group x by x.ReleaseYear;

//  processing on the created groups of the GroupBy method
Albums
	.GroupBy(a => a.ReleaseYear)  //  This method returns a collection of "mini collections"
	.Select(mc => new
	{
		Year = mc.Key,
		NumberOfAlbums = mc.Count()  // processing of "mini collection" data
	}) //  The Select is processing each mini collections one at a time. 
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	