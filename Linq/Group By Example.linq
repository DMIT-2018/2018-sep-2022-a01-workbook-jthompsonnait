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
//		b)	By a set of properties (anonymouse key set)  groupname.Key.PropertyName
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
	}); //  The Select is processing each mini collections one at a time. 

//  Query Syntax
//  Using this syntax you MUST specify the name you wish to use to refer to the
//   group (mini-collection) collection

from a in Albums
	// orderby a.ReleaseYear:  Would be valid because "a" is in cntext
	// orderby eachgPile.Key:  Would be invalid because "eachgPile" is not in context
group a by a.ReleaseYear into eachgPile
//  orderby a.ReleaseYear:  Would be invalid because "a" is out of vontext, the group name is eachgPile
// orderby eachgPile.Key:  Would be valid because "EachgPile" is currently in context and KEY is the release year
select new
{
	Year = eachgPile.Key, //  Key Component
	NumberofAlbums = eachgPile.Count()  //  processing of "mini-collection" data
};

//  Use a multiple set of criteria (properties) to form the group
//   also include a nested query to report on the "min-collection" (small pile) of the grouped data.

//  Display album group by ReleasedLabel, ReleaseYear.
//  Display the ReleasedYear and numbers of albums.
//  List only the years with 2 or more albums release.
//  For each album display the title, year of release and count of tracks

//  Original collection (large pile of data)  Albums
//  FIltering cannot be decided until the groups are created
//  Grouping: ReleaseLabel, ReleaseYear
//  Noe filter can be done on the group:  group.COunt >=2
//  Report the year and number of albums, newsted query to report
//  details per album:  Title, Year, # of tracks

Albums
	.GroupBy(a => new { a.ReleaseLabel, a.ReleaseYear }) //  Creating anonymous key set.
	.Where(eachgPile => eachgPile.Count() > 2)
	.OrderBy(eachgPile => eachgPile.Key.ReleaseLabel)
	.Select(eachgPile => new
	{
		Label = eachgPile.Key.ReleaseLabel,
		Year = eachgPile.Key.ReleaseYear,
		NumberofAlbums = eachgPile.Count(),
		AlbumsGroupItems = eachgPile  //  small pile (mini collection
							.Select(eachgPileInstance => new
							{
								Title = eachgPileInstance.Title,
								Year = eachgPileInstance.ReleaseYear,
								NumberOfTracks = eachgPileInstance.Tracks.Count()
								
							}
							)
		

	})







