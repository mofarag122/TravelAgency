using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.DTOs.HotelReservation
{
    public class HotelSpecParmas
    {
        public string? Sort { get; set; }

        public string? SearchedName { get; set; }

        public string? SearchedCountry { get; set; }

        public string? SearchedCity { get; set; }

        public string? SearchedRegion { get; set; }

    }
}
