using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities._Common;

namespace TravelAgency.Core.Domain.Entities.Identity
{
    public class User : Entity
    {
        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string HashedPassword { get; set; } = null!;

        public Address Address { get; set; } = null!;

        public Roles Role { get; set; } 
    }
}
