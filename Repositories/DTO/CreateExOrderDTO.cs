using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO
{
    public class CreateExOrderDTO
    {
        [MaxLength(250)]
        public string? Detail { get; set; }
        [Required(ErrorMessage = "Customer 2 Account is required")]
        public string Account2Id { get; set; }

        [Required(ErrorMessage = "Your Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Your Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Your Phone Number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Your CartItemId is required")]
        public string CartItemId { get; set; }
    }
}
