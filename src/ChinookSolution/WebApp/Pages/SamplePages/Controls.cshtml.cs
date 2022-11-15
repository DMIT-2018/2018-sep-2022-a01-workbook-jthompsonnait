#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WebApp.Pages.SamplePages
{
    public class ControlsModel : PageModel
    {
        [TempData]
        public string Feedback { get; set; }

        [BindProperty]
        public string EmailText { get; set; }
        [BindProperty]
        public string PasswordText { get; set; }

        [BindProperty] public DateTime DateText { get; set; } = DateTime.Today;
        [BindProperty] public TimeSpan TimeText { get; set; } = DateTime.Now.TimeOfDay;

        [BindProperty]
        public string Meal { get; set; }

        //  assume this array is actually data retrieved from the database
        public string[] Meals { get; set; } = new string[] { "breakfast", "lunch", "dinner", "snacks" };

        [BindProperty]
        public bool AcceptanceBox { get; set; }

        [BindProperty]
        public string MessageBody { get; set; }

        [BindProperty]
        public int MyRide { get; set; }

        //  pretend tat the following collection is data from a database
        //  The collection is based on a 2 property class called SelectionList
        //  The data for the list will be created in a separate method.
        public List<SelectionList> Rides { get; set; }

        [BindProperty]
        public string VacationSpot { get; set; }

        public List<string> VacationSpots { get; set; }

        [BindProperty]
        public int ReviewRating { get; set; }
        public IActionResult OnPostTextBox()
        {
            Feedback = $"Email {EmailText}; Password {PasswordText}; Date {DateText.ToShortDateString()}; Time {TimeText}";
            return Page();
        }
        public IActionResult OnPostRadioCheckArea()
        {
            Feedback = $"Meal {Meal}; Acceptance {AcceptanceBox}; Message {MessageBody}";
            return Page();
        }

        public IActionResult OnPostListSlider()
        {
            Feedback = $"Ride {MyRide}; Vacation {VacationSpot}; Review Rating {ReviewRating}";
            PopulatedList();
            return Page();
        }





        public void OnGet()
        {
            PopulatedList();
        }

        private void PopulatedList()
        {
            int i = 1;
            //  Create a pretend collection from the database represents different types
            //      of transportation (rides)
            Rides = new List<SelectionList>();
            Rides.Add(new SelectionList() { ValueID = i++, DisplayText = "Car" });
            Rides.Add(new SelectionList() { ValueID = i++, DisplayText = "Bus" });
            Rides.Add(new SelectionList() { ValueID = i++, DisplayText = "Bike" });
            Rides.Add(new SelectionList() { ValueID = i++, DisplayText = "Motorcycle" });
            Rides.Add(new SelectionList() { ValueID = i++, DisplayText = "Boat" });
            Rides.Add(new SelectionList() { ValueID = i++, DisplayText = "Plane" });
            Rides.Sort((x,y) => x.DisplayText.CompareTo(y.DisplayText));

            VacationSpots = new List<string>();
            VacationSpots.Add("California");
            VacationSpots.Add("Caribbean");
            VacationSpots.Add("Cruising");
            VacationSpots.Add("Europe");
            VacationSpots.Add("Florida");
            VacationSpots.Add("Mexico");

        }
    }

}
