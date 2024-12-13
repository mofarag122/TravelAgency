using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.DTOs.Identity
{
    public class AddressDto
    {
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = null!;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = null!;
    }
}
