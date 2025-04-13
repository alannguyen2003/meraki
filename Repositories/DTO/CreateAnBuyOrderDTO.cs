using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO
{
    public class CreateAnBuyOrderDTO
    {
        [Required(ErrorMessage = "All fields is required")]
        public string CartItemIds { get; set; }
    }
}
