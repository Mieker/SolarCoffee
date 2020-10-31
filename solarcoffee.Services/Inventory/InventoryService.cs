using System.Collections.Generic;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        public List<ProductInventory> GetCurrentInventory()
        {
            throw new System.NotImplementedException();
        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            throw new System.NotImplementedException();
        }

        public ProductInventory GetByProductId(int productId)
        {
            throw new System.NotImplementedException();
        }

        public void CreateSnapshot()
        {
            throw new System.NotImplementedException();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            throw new System.NotImplementedException();
        }
    }
}