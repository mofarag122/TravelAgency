using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.DTOs.Identity
{
    public class UserToLoginDto
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
