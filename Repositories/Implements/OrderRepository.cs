using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.DatabaseConnection;
using Repositories.DTO;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MerakiDbContext _context;

        public OrderRepository(MerakiDbContext context)
        {
            _context = context;
        }

        // For orderDetail
        public async Task<Order> GetOrderByAccountId(string accountId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Account1Id == accountId || o.Account2Id == accountId);
        }
        // for orderDetail
        public async Task<string> GetLatestOrderIdAsync()
        {
            try
            {

                // Fetch the relevant data from the database
                var orderIds = await _context.Orders
                    .Select(u => u.OrderId)
                    .ToListAsync();

                // Process the data in memory to extract and orderDetail by the numeric part
                var latestOrderId = orderIds
                    .Select(id => new { OrderId = id, NumericPart = int.Parse(id.Substring(1)) })
                    .OrderByDescending(u => u.NumericPart)
                    .ThenByDescending(u => u.OrderId)
                    .Select(u => u.OrderId)
                    .FirstOrDefault();

                return latestOrderId;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<Order> CreateOrder(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error at OrderRepository: {ex.InnerException}");
            }
        }

        public async Task<dynamic> UpdateOrder(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error at OrderRepository: {ex.Message}");
            }
        }

        public async Task<dynamic> DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<string> AutoGenerateOrderId()
        {
            string newOrderId = "";
            string latestOrderId = await GetLatestOrderIdAsync();
            if (string.IsNullOrEmpty(latestOrderId))
            {
                newOrderId = "O000000001";
            }
            else
            {
                int numericpart = int.Parse(latestOrderId.Substring(1));
                int newnumericpart = numericpart + 1;
                newOrderId = $"O{newnumericpart:d9}";
            }
            return newOrderId;
        }

        // for cart
        public async Task<List<Order>> GetListOrderNotPaymentByAccountIdAsync(string accountId)
        {
            var listOrder = await _context.Orders
                .Where(order => order.Account1Id.Equals(accountId) || order.Account2Id.Equals(accountId) &&
                                order.Status == 1 &&
                                order.PaymentStatus == 0)
                .ToListAsync();
            return listOrder;
        }

        public async Task<double> GetTotalMoneyOfOrder(string orderId)
        {
            var orderDetails = await _context.OrderDetails
                                            .Where(item => item.OrderId.Equals(orderId))
                                            .ToListAsync();
            var order = await _context.Orders
.FirstOrDefaultAsync(order => order.OrderId.Equals(orderId) && order.OrderType == "Buy");
            double totalMoney = (double)0;
            foreach (var item in orderDetails)
            {
                var product1 = await _context.Products
                                    .FirstOrDefaultAsync(p => p.ProductId.Equals(item.ProductId));

                totalMoney += (double)(product1.ProductPrice * (decimal)item.Quantity);

                order.TotalMoney = totalMoney;
                _context.Attach(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return totalMoney;
        }

        public async Task<Order> GetOrderById(string orderId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(item => item.OrderId.Equals(orderId)) ?? new Order();
        }

        public async Task<Order> GetOrderByAccountIdAsync(string? accountId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Account1Id == accountId || o.Account2Id == accountId);
        }
        public async Task<List<Order>> GetOrdersByTypeAsync(string orderType)
        {
            return await _context.Orders.Include(o => o.Account1).Include(o => o.Account2).Include(o => o.OrderDetails).Where(o => o.OrderType == orderType).ToListAsync();
        }
        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<int> GetNumberOfOrders()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<int> GetNumberOfOrderBasedOnStatus(int status)
        {
            return await _context.Orders.CountAsync(item => item.Status == status);
        }
        public async Task<dynamic> GetNumberOrderOfCustomerByStatus(string accountId, int status)
        {
            return await _context.Orders.CountAsync(item => item.Account1Id == accountId || item.Account2Id == accountId && item.Status == status);
        }
        public async Task<dynamic> GetNumberOrderOfCustomer(string accountId)
        {
            return await _context.Orders.CountAsync(item => item.Account1Id == accountId || item.Account2Id == accountId);
        }
        public async Task<double> GetEarningOnAllOrders(string accountId)
        {
            var orders = await _context.Orders
                .Where(item => item.Account1Id.Equals(accountId) || item.Account2Id.Equals(accountId) &&
                                item.Status >= 4).ToListAsync();
            var totalEarnings = (double)0;
            foreach (var order in orders)
            {
                totalEarnings += order.TotalMoney;
            }
            return totalEarnings;
        }


        // For orderDetail detail

        /// REVIEW
        public async Task<OrderDetail> GetOrderDetailById(string orderDetailId)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(item => item.OrderDetailId.Equals(orderDetailId));
        }
        public async Task<OrderDetail> GetOrderDetailByOrderIdAndAccountId(string orderId, string accountId)
        {
            return await _context.OrderDetails.Include(od => od.ProductId).FirstOrDefaultAsync(item => item.OrderId.Equals(orderId) && item.AccountId.Equals(accountId));
        }
        public async Task<List<OrderDetail>> GetOrderDetailsByOrderId(string orderId)
        {
            return await _context.OrderDetails
                .Where(item => item.OrderId.Equals(orderId))
                .ToListAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByProductId(string productId)
        {
            return await _context.OrderDetails
                .Where(item => item.ProductId.Equals(productId))
                .ToListAsync();
        }


        public async Task<string> GetLatestOrderDetailIdAsync()
        {
            try
            {

                // Fetch the relevant data from the database
                var orderDetailIds = await _context.OrderDetails
                    .Select(u => u.OrderDetailId)
                    .ToListAsync();

                // Process the data in memory to extract and orderDetail by the numeric part
                var latestOrderDetailId = orderDetailIds
                    .Select(id => new { OrderDetailId = id, NumericPart = int.Parse(id.Substring(2)) })
                    .OrderByDescending(u => u.NumericPart)
                    .ThenByDescending(u => u.OrderDetailId)
                    .Select(u => u.OrderDetailId)
                    .FirstOrDefault();

                return latestOrderDetailId;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<dynamic> GetOrderDetailsOfCustomer(string accountId)
        {
            return await _context.OrderDetails.Where(o => o.AccountId == accountId).Select(od => new OrderDetailDTO
            {
                OrderDetailId = od.OrderDetailId,
                OrderId = od.OrderId,
                ProductName = od.Product.ProductName,
                Quantity = od.Quantity,
                PaidPrice = od.PaidPrice,
                ProductImage = od.Product.Images,
                OrderNumber = od.OrderNumber,
                AccountId = od.Product.AccountId,
            }).ToListAsync();
        }

        //public async Task<List<OrderDetail>> GetListCartItemOfAccount(string accountId)
        //{
        //    var listOrderDetail = await _context.OrderDetails
        //        .Include(od => od.Product)
        //        .Where(od => od.AccountId.Equals(accountId))
        //        .ToListAsync();
        //    return listOrderDetail;
        //}

        public async Task<string> AutoGenerateOrderDetailId()
        {
            string newOrderDetailId = "";
            string latestOrderDetailId = await GetLatestOrderDetailIdAsync();
            if (string.IsNullOrEmpty(latestOrderDetailId))
            {
                newOrderDetailId = "OD00000001";
            }
            else
            {
                int numericpart = int.Parse(latestOrderDetailId.Substring(2));
                int newnumericpart = numericpart + 1;
                newOrderDetailId = $"OD{newnumericpart:d8}";
            }
            return newOrderDetailId;
        }

        /// CREATE - UPDATE -DELETE
        public async Task<OrderDetail> CreateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                await _context.OrderDetails.AddAsync(orderDetail);
                var result = await _context.SaveChangesAsync();
                return result > 0 ? orderDetail : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error at OrderRepository: {ex.InnerException}");
            }
        }

        public async Task<dynamic> UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                _context.OrderDetails.Update(orderDetail);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error at OrderRepository: {ex.Message}");
            }
        }

        public async Task<dynamic> UpdateOrderDetails(List<OrderDetail> orderDetails)
        {
            try
            {
                foreach (var detail in orderDetails)
                {
                    _context.OrderDetails.Update(detail); // Assuming OrderDetails is the correct DbSet for order details
                }
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error at OrderRepository: {ex.Message}");
            }
        }
        public async Task<dynamic> DeleteOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Remove(orderDetail);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<OrderDetail> GetCartItemByProductIdAndAccountAsync(string accountId, string productId)
        {
            var cartItem = await _context.OrderDetails.Include(od => od.Product)
                .FirstOrDefaultAsync(od => od.AccountId.Equals(accountId) && od.ProductId.Equals(productId));

            return cartItem;
        }

        public async Task<List<OrderDetail>> GetListCartItemByIdsString(string cartItemIdsString)
        {
            try
            {
                if (string.IsNullOrEmpty(cartItemIdsString))
                {
                    return new List<OrderDetail>(); // Return an empty list if the input is null or empty
                }

                var cartItemIds = cartItemIdsString.Split(',')
                                                   .Select(id => id.Trim())
                                                   .ToList();
                //var cartItems = await _context.OrderDetails
                //                              .Where(od => cartItemIds.Contains(od.OrderDetailId.ToString())) 
                //                              .ToListAsync();
                // Convert the list into a format suitable for SQL IN clause
                var formattedIds = string.Join("','", cartItemIds); // Format IDs for SQL IN clause
                var query = $"SELECT * FROM OrderDetails WHERE OrderDetailId IN ('{formattedIds}')";

                // Execute the raw SQL query to fetch OrderDetails
                var cartItems = await _context.OrderDetails.FromSqlRaw(query).ToListAsync();
                return cartItems;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error in GetListCartItemByIdsString: {ex.Message}");
            }

        }
        public async Task DetachEntityAsync(object item)
        {
            _context.Entry(item).State = EntityState.Detached;
        }

    }
}
