using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTO;
using Services.Interfaces;
using System.Security.Claims;

namespace Meraki_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        
        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create-buy-order")]
        public async Task<IActionResult> CreateBuyOrderFromCartItemSelected([FromForm] CreateAnBuyOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            if (orderDTO == null)
            {
                return BadRequest("All fields must be filled in");
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;
            var result = await _orderService.CreateAnOrderBuyFromCartAsync(orderDTO, userEmail);

            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("require-exchange-order")]
        public async Task<IActionResult> RequireAnExchangeOrderAsync([FromForm] CreateExOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            if (orderDTO == null)
            {
                return BadRequest("All fields must be filled in");
            }            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;
            var result = await _orderService.RequireAnExchangeOrderAsync(orderDTO, userEmail);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("accept-exchange-order")]
        public async Task<IActionResult> AcceptRequiredExchangeOrderAsync(string orderId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            if (orderId == null)
            {
                return BadRequest("Order Id is required");
            }            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;
            var result = await _orderService.AcceptRequiredExchangeOrderAsync(orderId, userEmail);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("rufuse-exchange-order")]
        public async Task<IActionResult> RefuseRequiredExchangeOrderAsync(string orderId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            if (orderId == null)
            {
                return BadRequest("Order Id is required");
            }            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;
            var result = await _orderService.RefuseRequiredExchangeOrderAsync(orderId, userEmail);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create-exchange-order")]
        public async Task<IActionResult> CreateAnExchangeOrderAsync(string orderId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            if (orderId == null)
            {
                return BadRequest("Order Id is required");
            }            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;
            var result = await _orderService.CreateAnOrderForExchangeAsync(orderId, userEmail);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("total-money-order")]
        public async Task<IActionResult> RetrieveTotalMoneyByOrderIdAsync(string orderId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            if (orderId == null)
            {
                return BadRequest("Order Id is required");
            }
            var result = await _orderService.RetrieveTotalMoneyByOrderId(orderId);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("finish-delivering-stage")]
        public async Task<IActionResult> FinishDeliveringStageAsync(string orderId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            if (orderId == null)
            {
                return BadRequest("Order Id is required");
            }
            var result = _orderService.FinishDeliveringStage(orderId);
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("list-of-orders")]
        public async Task<IActionResult> GetAllOrderAsync(string orderId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            if (orderId == null)
            {
                return BadRequest("Order Id is required");
            }
            var result = await _orderService.GetAllOrders();
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("list-of-all-order-by-type")]
        public async Task<IActionResult> GetOrdersByTypeAsync(string orderId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            if (orderId == null)
            {
                return BadRequest("Order Id is required");
            }
            var result = await _orderService.GetOrdersByTypeAsync(orderId);
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("numbers-of-all-orders")]
        public async Task<IActionResult> GetNumbersOfOrdersAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }

            var result = await _orderService.GetNumberOfOrders();
            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("numbers-of-all-orders-with-status")]
        public async Task<IActionResult> GetNumbersOfOrdersBasedOnStatusAsync(int status)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }

            var result = await _orderService.GetNumberOfOrderBasedOnStatus(status);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("numbers-of-customer-by-status")]
        public async Task<IActionResult> GetNumberOrdersOfCustomerByStatusAsync(int status)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;

            var result = await _orderService.GetNumberOrderOfCustomerByStatus(status, userEmail);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("numbers-of-customer")]
        public async Task<IActionResult> GetNumberAllCustomerOfCustomerAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;

            var result = await _orderService.GetNumberOrderOfCustomer(userEmail);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("total-earnings")]
        public async Task<IActionResult> GetTotalEarningAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;

            var result = await _orderService.GetTotalEarnings(userEmail);
            return Ok(result);
        }
        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("checkout-order")]
        public async Task<IActionResult> CreateOrderForCheckout(CheckoutRequest request)
        {
            await _orderService.CheckoutRequest(request);
            return Ok(new ApiResponse()
            {
                StatusCode = 200,
                Message = "Checkout successful! Please call API Payment for the next steps!"
            });
        }


    }
}
