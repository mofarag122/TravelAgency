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
        void AddReservation(HotelReservation hotelReservation);

        public HotelReservation GetReservation(int reservationId);
        public List<HotelReservation?> GetReservations(int hotelId, int roomId);

        public List<HotelReservation?> GetUserReservations(int userId);

        public void UpdateReservation(HotelReservation hotelReservation);

        public void DeleteReservation(int reservationId);
    }
}
