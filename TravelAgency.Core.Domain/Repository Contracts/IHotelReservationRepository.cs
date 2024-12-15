using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;

namespace TravelAgency.Core.Domain.Repository_Contracts
{
    public interface IHotelReservationRepository
    {
        void AddHotelReservation(HotelReservation hotelReservation);

    }
}
