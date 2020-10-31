using System.Collections.Generic;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        public List<SalesOrder> GetOrders()
        {
            throw new System.NotImplementedException();
        }

        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}