using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities._Common;

namespace TravelAgency.Core.Domain.Entities.Identity
{
    public class Authentication : Entity
    {
        public int UserId { get; set; }

        public string? Token { get; set; }

    }
}
