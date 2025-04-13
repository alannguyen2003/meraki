using Repositories.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICartService
    {
        public Task<dynamic> AddToCartAsync(AddToCartDTO cartDTO, string customerEmail);
        public Task<dynamic> UpdateCartItemAsync(string cartItemId, double? quantity, string customerEmail);
        public Task<dynamic> DeleteCartItemAsync(string cartItemId, string customerEmail);
        public Task<dynamic> GetCartListAsync(string customerEmail);
    }
}
