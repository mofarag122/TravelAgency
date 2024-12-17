using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.HotelReservation;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;

namespace TravelAgency.Core.Application.Service_Contracts
{
    public interface IHotelReservationService
    {
        List<HotelToReturnDto> GetAllHotels(string? token);
        public List<RoomToReturnDto> GetRooms(string? token, int Hotelid);
        public string ReserveRoom(string? token, ReservationToCreateDto reservationDto);
    }
}
