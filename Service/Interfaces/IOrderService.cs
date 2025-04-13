using Repositories.DTO;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        public Task<dynamic> RequireAnExchangeOrderAsync(CreateExOrderDTO orderDTO, string customerEmail);
        public Task<dynamic> AcceptRequiredExchangeOrderAsync(string orderId, string customerEmail);
        public Task<dynamic> RefuseRequiredExchangeOrderAsync(string orderId, string customerEmail);
        public Task<dynamic> CreateAnOrderForExchangeAsync(string orderId, string customerEmail);
        public Task<dynamic> CreateAnOrderBuyFromCartAsync(CreateAnBuyOrderDTO orderDTO, string customerEmail);

        public Task<double> RetrieveTotalMoneyByOrderId(string orderId);
        public Task FinishDeliveringStage(string orderId);
        public Task<List<Order>> GetAllOrders();
        public Task<List<Order>> GetOrdersByTypeAsync(string orderType);
        public Task<int> GetNumberOfOrders();
        public Task<int> GetNumberOfOrderBasedOnStatus(int status);
        public Task<dynamic> GetNumberOrderOfCustomerByStatus(int status, string customerEmail);
        public Task<dynamic> GetNumberOrderOfCustomer(string customerEmail);
        public Task<double> GetTotalEarnings(string customerEmail);
        public Task CheckoutRequest(CheckoutRequest request);
    }
}
