using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;

namespace TravelAgency.Core.Domain.Specifications_DP.Hotel_Specs
{
    public class RoomsFilterationSpecifation : BaseSpecifications<Room>
    {
        public RoomsFilterationSpecifation(decimal? price, string? type, string? sort)
        : base
            (
                     r =>
                      (price == null || r.PricePerNight == price)
                                    &&
                     (string.IsNullOrEmpty(type) || (ParseRoomType(type) == r.RoomType))
            )

        {
            if (sort == "priceDesc")
                AddOrderByDesc(r => r.PricePerNight);
            else
                AddOrderBy(r => r.PricePerNight);


        }

        private static RoomTypes? ParseRoomType(string? type)
        {
            if (string.IsNullOrEmpty(type))
                return null;

            if (Enum.TryParse<RoomTypes>(type, out var parsedRoomType))
                return parsedRoomType;

            return null; 
        }
    }
}
