using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Domain.Entities.Identity
{
    public class Address
    {
        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;
    }
}
