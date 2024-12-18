using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;

namespace TravelAgency.Core.Domain.Specifications_DP
{
    public class HotelFilterationSpecifications : BaseSpecifications<Hotel>
    {
        public HotelFilterationSpecifications(string? sort , string? searchedName , string? searchedCountry , string? searchedCity , string? searchedRegion)
       
            :base
            (
              h =>
                     (string.IsNullOrEmpty(searchedName) || h.Name.Contains(searchedName))
                                        &&
                     (string.IsNullOrEmpty(searchedCountry) || h.Location.Country.Contains(searchedCountry))
                                        &&
                     (string.IsNullOrEmpty(searchedCity) || h.Location.City.Contains(searchedCity))
                                        &&
                     (string.IsNullOrEmpty(searchedRegion) || h.Location.Region.Contains(searchedRegion))
            )
        {

           
            if (sort == "nameDesc")
                AddOrderByDesc(h => h.Name);
            else
                AddOrderBy(h => h.Name);

            

        }
    }
}
