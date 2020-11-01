using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public OrderService(SolarDbContext db, ILogger<OrderService> logger,
            IProductService productService, IInventoryService inventoryService)
        {
            _db = db;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }
        
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(order => order.Customer)
                    .ThenInclude(customer => customer.PrimaryAddress)
                .Include(order => order.SalesOrderItems)
                    .ThenInclude(item => item.Product)
                .ToList();
        }

        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            _logger.LogInformation("Generating new order");
            
            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);

                var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;

                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = "Open order created",
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

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var order = _db.SalesOrders.Find(id);
            order.UpdatedOn = DateTime.UtcNow;
            order.IsPaid = true;
            
            try
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = $"Order {order.Id} closed: Invoice paid in full.",
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