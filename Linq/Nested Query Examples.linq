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
	//  Nested Queries
	//  Sometime referred to as sub queries

	//  Simply put:  it is a query within a query [query witin that query...]

	//  List all sales support employees showing their
	//  Fullname (last, first), Title, Phone
	//  For each employee show a list of their customers
	//  List the customers FullName (last, first), City, State.

	// Smith, John Sales Support 7801234567	//  This is the employee
	//		Kan, Jerry Edmonton Ab			//  Customer
	//		Apple, Cindy Calgary Ab			//  Customer

	//  Tomson, Sue Sales Support 7805551212
	//		Gates, Bill Seattle Wa
	//		Job, Steve	LA Ca

	//  ETC
	//		ETC

	//  There appears to be 2 separate list that needs to be within one final
	//   collection.
	//	One for the employees
	//  One for the customers.

	//  Concerns:  The list are inter mixed!!!!

	//  C# point of view in a class definition
	//  A composite class can have a single occurring field AND use of other classes.
	//  OTHER classes maybe a single instance OR collection<T>
	//  List<T>, IEnumerable<T>, IQueryable<T> is a collection with a difine datatype of <T>
	
	//  ClassName
	//  Property
	//  Property
	//  Collection<T> (set of records, but it is still a property)

	var results = Employees
		.Where(e => e.Title.Contains("Sales Support"))
		.Select(e => new EmployeeItem
		{
			FullName = e.LastName + ", " + e.FirstName,
			Title = e.Title,
			Phone = e.Phone,
			CustomerList = Customers
							.Where(c => c.SupportRepId == e.EmployeeId)
							.Select(c => new CustomerItem
							{
								FullName = c.LastName + ", " + c.FirstName,
								City = c.City,
								State = c.State
							}).ToList()
		}).Dump();
}

//  You can define..
public class EmployeeItem
{
	public string FullName { get; set; }
	public string Title { get; set; }
	public string Phone { get; set; }
	public IEnumerable<CustomerItem> CustomerList {get; set;}
}

public class CustomerItem
{
	public string FullName { get; set; }
	public string City { get; set; }
	public string State { get; set; }

}

