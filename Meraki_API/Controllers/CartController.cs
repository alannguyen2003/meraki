using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTO;
using Services.Interfaces;
using System.Security.Claims;

namespace Meraki_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCartAsync([FromForm] AddToCartDTO cartDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;
            if (cartDTO == null)
            {
                return BadRequest("All field must be filled");
            }
            var result = await _cartService.AddToCartAsync(cartDTO, userEmail);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("delete-cart-item")]
        public async Task<IActionResult> DeleteCartItemAsync(string cartItemId)
        {
            if (cartItemId == null)
            {
                return BadRequest("Cart Item Id is required");
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;
            var result = await _cartService.DeleteCartItemAsync(cartItemId, userEmail);
            if (result)
            {
                return Ok("Delete successfully");
            }
            return BadRequest(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("update-cart-item")]
        public async Task<IActionResult> UpdateCartItemAsync(string orderDetailId, double quantity)
        {
            if (orderDetailId == null)
            {
                return BadRequest("Cart Item Id is required");
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;
            var result = await _cartService.UpdateCartItemAsync(orderDetailId, quantity, userEmail);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("list-cart-items")]
        public async Task<IActionResult> GetListCartItems()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userEmail = identity.Claims.FirstOrDefault().Value;

            var result = await _cartService.GetCartListAsync(userEmail);
            return Ok(result);
        }

    }
}
