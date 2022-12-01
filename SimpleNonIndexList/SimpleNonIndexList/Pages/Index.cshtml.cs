using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleNonIndexList.ViewModel;

namespace SimpleNonIndexList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty] public List<Employee> Employees { get; set; } = new List<Employee>();

        [BindProperty] public int RemoveEmployeeID { get; set; }

        [BindProperty] public string EmployeeName { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            //Employees.Add(new Employee() { EmployeeId = 1, Name = "James" });
        }

        public void OnGet()
        {

        }

        public void OnPostRemoveEmployee()
        {
            var selectedItem = Employees.SingleOrDefault(x => x.EmployeeId == RemoveEmployeeID);
            if (selectedItem != null)
            {
                Employees.Remove(selectedItem);
            }
        }

        public void OnPostAddToEmployeeList()
        {
            int maxId = Employees.Count == 0
                ? 1
                : Employees.OrderBy(x => x.EmployeeId)
                    .Max(x => x.EmployeeId) + 1;
            Employees.Add(new Employee() { EmployeeId = maxId, Name = EmployeeName });
            
        }
    }
}