#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthWind.BLL;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ProductServices _productServices;

        //  command model (Will be updating the table/entity with these records/items)
        [BindProperty] public List<NorthWind.ViewModel.ProductInfo> NewSaleItems { get; set; } = new();

        //  query model (This is the initial data that we received from our entities)
        [BindProperty] public List<NorthWind.ViewModel.ProductInfo> Items { get; set; }

        //  product id that is used to select from my items and transfer to my sales itesm
        [BindProperty] public int SelectedProductID { get; set; }

        //  product id that is used to select the command model on my sale items and update he totals (qty * price)
        [BindProperty] public int RefreshProductID { get; set; }

        //  product id that is used to remove the command model from my sales item and then used to update my item query model
        [BindProperty] public int RemoveProductID { get; set; }




        public IndexModel(ILogger<IndexModel> logger, ProductServices productServices)
        {
            _logger = logger;
            _productServices = productServices;
        }

        public void OnGet()
        {
            //  Update my items query model
            Items = _productServices.GetProducts();
        }

        //  Add command model items to sales list
        public IActionResult OnPostAddItem()
        {
            // selectedItem holds the value of the pressed button
            var selectedItem = Items.SingleOrDefault(x => x.ProductID == SelectedProductID);
            if (selectedItem != null)
            {
                //  remove the item (query model) from the inventory list
                Items.Remove(selectedItem);
                //  update the item/command model with initial values
                selectedItem.Quantity = 1;
                selectedItem.Total = selectedItem.UnitPrice * selectedItem.Quantity;
                //  add item (command model) to  sale item list
                NewSaleItems.Add(selectedItem);
            }
            return Page();
        }

        //  remove command model item from sale item list and refresh the query modem inventory list with a clean record
        public IActionResult OnPostRemoveItem()
        {
            //  get my query model
            var selectedItem = NewSaleItems.SingleOrDefault(x => x.ProductID == RemoveProductID);
            if (selectedItem != null)
            {
                //  remove the item (query model) from the items list
                NewSaleItems.Remove(selectedItem);
                Items.Add(_productServices.GetProductByProductID(RemoveProductID));
                Items = Items.OrderBy(x => x.ProductName).ToList();
            }
            return Page();
        }

        public IActionResult OnPostRefreshItem()
        {
            var selectedItem = NewSaleItems.SingleOrDefault(x => x.ProductID == RefreshProductID);
            if (selectedItem != null)
            {
                selectedItem.Total = selectedItem.UnitPrice * selectedItem.Quantity;
            }
            return Page();
        }
    }
}