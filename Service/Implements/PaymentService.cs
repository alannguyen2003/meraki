using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;
using Repositories.DTO;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implements
{
    public class PaymentService : IPaymentService
    {
        private readonly PayOS _payOs;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        private readonly IAccountRepository _accountRepository;

        public PaymentService(IConfiguration configuration,
                    ITransactionRepository transactionRepository,
                    IOrderRepository orderRepository,
                    IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _payOs = new PayOS(_configuration.GetSection("PayOS:ClientID").Value!,
                    _configuration.GetSection("PayOS:ApiKey").Value!,
                    _configuration.GetSection("PayOS:ChecksumKey").Value!);
            _transactionRepository = transactionRepository;
            _orderRepository = orderRepository;
            _accountRepository = accountRepository;
        }

        public async Task<CreatePaymentResult> CreatePaymentLink(string orderId, string userId)
        {
            //Update total money before generate payment link
            var totalMoney = await _orderRepository.GetTotalMoneyOfOrder(orderId);
            var user = await _accountRepository.GetAccountById(userId);
            var transaction = await _transactionRepository.CreateTransaction(orderId, userId);
            var listItem = new List<ItemData>()
                {
                    new ItemData("Thanh toán đơn hàng", 1, (int)totalMoney * 1000)
                };
            PaymentData paymentData = new PaymentData(Int32.Parse(transaction.TransactionId.Substring(2)), (int)totalMoney * 1000,
                "Meraki - Exchange Things Platform", listItem,
                "http://localhost:7258/checkout/fail?transactionId=" + transaction.TransactionId,
                "http://localhost:7258/checkout/success?transactionId=" + transaction.TransactionId);
            CreatePaymentResult createPaymentResult = await _payOs.createPaymentLink(paymentData);
            return createPaymentResult;
        }

        ////0 - On going
        ////1 - Payed success
        ////2 - Cancel Payment
        public async Task FailedPayment(string transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionById(transactionId);
            transaction.Status = "Failed";
            transaction.CreatedAt = DateTime.Now;
            var order = await _orderRepository.GetOrderById(transaction.OrderId);
            order.PaymentStatus = 2;
            await _orderRepository.UpdateOrder(order);
            await _transactionRepository.UpdateTransaction(transaction);
        }

        public async Task SuccessPayment(string transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionById(transactionId);
            transaction.Status = "Success";
            transaction.CreatedAt = DateTime.Now;
            await _transactionRepository.UpdateTransaction(transaction);
            var order = await _orderRepository.GetOrderById(transaction.OrderId);
            order.Status = 4;
            order.PaymentStatus = 1;
            await _orderRepository.UpdateOrder(order);
        }

        // For exchange thing to thing
        public async Task<dynamic> CreatePaymentLinkForOrderExchange(string orderId, string userId)
        {
            //Update total money before generate payment link
            var order = await _orderRepository.GetOrderById(orderId);
            if (order.OrderType != "Exchange")
            {
                return new ApiResponse()
                {
                    StatusCode = 400,
                    Message = "This order is not exchange order"
                };
            }
            var totalMoney = await _orderRepository.GetTotalMoneyOfOrder(orderId);
            var user = await _accountRepository.GetAccountById(userId);
            var transaction = await _transactionRepository.CreateTransaction(orderId, userId);
            var listItem = new List<ItemData>()
                {
                    new ItemData("Thanh toán đơn hàng", 1, (int)totalMoney * 1000)
                };
            PaymentData paymentData = new PaymentData(Int32.Parse(transaction.TransactionId.Substring(2)), (int)totalMoney * 1000,
                "Meraki - Exchange Things Platform", listItem,
                "http://localhost:7258/checkout/fail?transactionId=" + transaction.TransactionId,
                "http://localhost:7258/checkout/success?transactionId=" + transaction.TransactionId);
            CreatePaymentResult createPaymentResult = await _payOs.createPaymentLink(paymentData);
            return createPaymentResult;
        }


    }
}
