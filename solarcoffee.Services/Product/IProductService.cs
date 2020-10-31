

using System.Collections.Generic;

namespace SolarCoffee.Services.Product
{
    public interface IProductService
    {
        List<Data.Models.Product> GetAllProducts();
        Data.Models.Product GetProductById(int id);
        ServiceResponse<bool> CreateProduct(Data.Models.Product product);
        // bool ArchiveProduct(Data.Models.Product product);
        ServiceResponse<bool> ArchiveProduct(int id);
    }
}