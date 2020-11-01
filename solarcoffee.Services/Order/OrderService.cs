using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<OrderService> _logger;

        public OrderService(SolarDbContext db, ILogger<OrderService> logger)
        {
            _db = db;
            _logger = logger;
        }
        
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(order => order.Customer)
                .ThenInclude(customer => customer.PrimaryAddress)
                .Include(order => order.SalesOrderItems)
                .ToList();
        }

        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            try
            {
                var order = _db.SalesOrders.Find(id);
                order.IsPaid = true;
                _db.SalesOrders.Update(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = "Orders fulfilled marked",
                    Data = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Data = false
                };
            }
            
        }
    }
}