using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.SamplePages
{
    public class BasicsModel : PageModel
    {
        //  Basically this is an object, treat it as such

        //  data fields

        //  properties
        //  The annotation {TempData] stores data until it's read in another
        //      immediate request
        //  This annotation attribute has two method called Keep(string) and
        //      peek(string) (used on Content page).
        //  Keep in a dictionary (name/value pair)
        //  Useful to redirect when data is required for more than a single request
        //  Implemented by TempData providers using either cookies or session state
        //  TempData is NOT bound to any particular control like BindProperty
        [TempData]
        public string FeedBack { get; set; }

        //  The annotation BindProperty ties a property in the PageModel Class
        //      directly to a control on the Content Page
        //  Data is transferred between the two automatically
        //  On the Content page, the control to use this property will have
        //      a helper-tag called asp-for
        [BindProperty]
        public int ID { get; set; }
        public string MyName { get; set; }



        //  constructors

        //  behaviours (aka methods)

        public void OnGet()
        {
            //  Execute in response to a Get Request from the browser
            //  When the page is "first" accessed, the browser issue a Get request
            //  When the page is refreshed, WITHOUT a post request, the browser issues a
            //      Get Request
            //  When the page is retrieved in response to a forms's post, using
            //      RedirectToPage()
            //  IF NOT RedirectToPage() is used on the POST, there is NO Get requested issue

            Random rnd = new Random();
            int oddEven = rnd.Next(0, 25);
            if (oddEven % 2 == 0)
            {
                MyName = $"James is even {oddEven}";
            }
            else
            {
                MyName = null;
            }
        }
    }
}
