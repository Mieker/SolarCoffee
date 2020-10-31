using System.Collections.Generic;

namespace solarcoffee.Services.Product
{
    public interface IProductService
    {
        List<SolarCoffee.Data.Models.Product> GetAllProducts();
        

    }
}