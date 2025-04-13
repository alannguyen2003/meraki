using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTO;
using Services.Interfaces;

namespace Meraki_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactions();
            return Ok(new ApiResponse()
            {
                StatusCode = 200,
                Message = "get all transaction successful!",
                Data = transactions
            });
        }

        [HttpGet("get-all-by-order")]
        public async Task<IActionResult> GetAllTransactionByOrder(string orderId)
        {
            var transactions = await _transactionService.GetAllTransactionByOrder(orderId);
            return Ok(new ApiResponse()
            {
                StatusCode = 200,
                Data = transactions
            });
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetTransactionById(string transactionId)
        {
            var transactions = await _transactionService.GetTransactionById(transactionId);
            return Ok(new ApiResponse()
            {
                StatusCode = 200,
                Data = transactions
            });
        }

        [HttpPost("create-new-transaction")]
        public async Task<IActionResult> CreateNewTransaction(string orderId, string accountId)
        {
            var transaction = await _transactionService.CreateTransaction(orderId, accountId);

            return Ok(new ApiResponse()
            {
                StatusCode = 201,
                Data = transaction
            });
        }
    }
}
