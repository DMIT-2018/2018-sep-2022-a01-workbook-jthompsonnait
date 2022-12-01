#nullable disable
using Microsoft.EntityFrameworkCore.Infrastructure;
using NorthWind.DAL;
using NorthWind.ViewModel;

namespace NorthWind.BLL
{
    public class ProductServices
    {
        //  This class needs to be accessed by an "outside user" (WebApp)
        //  Therefore, the class needs to be public

        #region Constructor and COntext Dependency

        private readonly NorthwindContext _context;

        internal ProductServices(NorthwindContext context)
        {
            _context = context;
        }

        #endregion

        #region Services

        //  Services are methods

        //  Query to obtain the product data
        public List<ViewModel.ProductViewModel> GetProducts()
        {
            return _context.Products
                .OrderBy(x => x.ProductName)
                .Select(x => new ViewModel.ProductViewModel()
                {
                    ProductID = x.ProductID,
                    ProductName = x.ProductName,
                    UnitPrice = (int)x.UnitPrice
                }
                )
                .Take(15)
                .ToList();
        }

        public ViewModel.ProductViewModel GetProductByProductID(int productID)
        {
            return _context.Products
                .Where(x => x.ProductID == productID)
                .OrderBy(x => x.ProductName)
                .Select(x => new ViewModel.ProductViewModel()
                    {
                        ProductID = x.ProductID,
                        ProductName = x.ProductName,
                        UnitPrice = (int)x.UnitPrice
                    }
                ).SingleOrDefault();
        }
        #endregion
    }
}
