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
        public List<HotelToReturnDto> GetAllHotels(string? token);
        public List<HotelToReturnDto> GetHotels(string? token, HotelSpecParmas hotelSpecs);
        public HotelToReturnDto GetHotel(string? token, int hotelId);

        public RoomToReturnDto GetRoom(string? token, int hotelId, int roomId);
        public List<RoomToReturnDto> GetRooms(string? token, int hotelId, RoomSpecParams roomSpecs);
        public bool ReserveRoom(string? token, ReservationToCreateDto reservationDto);
    }
}
