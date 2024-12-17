using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Domain.Entities.Hotel_Reservation
{
    public class Location
    {
        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Region { get; set; } = null!;
    }
}
