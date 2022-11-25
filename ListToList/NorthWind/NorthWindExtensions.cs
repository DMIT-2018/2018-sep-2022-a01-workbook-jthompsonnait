

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NorthWind.BLL;
using NorthWind.DAL;

namespace NorthWind
{
    public static class NorthWindExtensions
    {   
        public static void NorthWindBackendDependencies(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            //  Register the DBContext class in Chinook2018 with the service collection
            services.AddDbContext<NorthwindContext>(options);

            //  Add any services that you create in the class library
            //  using .AddTransient<t>(...)

            services.AddTransient<ProductServices>((serviceProvider) =>
                {
                    var context = serviceProvider.GetRequiredService<NorthwindContext>();
                    //  Create an instance of the service and return the instance
                    return new ProductServices(context);
                }
            );
        }
    }
}
