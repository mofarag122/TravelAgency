using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.HotelReservation;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Core.Application.Services
{
    public class HotelReservationService : IHotelReservationService
    {
        private IHotelRepository _hotelRepository;
        private IHotelReservationRepository _reservationRepository;

        public HotelReservationService(IHotelRepository hotelRepository , IHotelReservationRepository reservationRepository)
        {
            _hotelRepository = hotelRepository;
            _reservationRepository = reservationRepository;
        }

        public List<HotelToReturnDto> GetAllHotels()
        {
            List<Hotel> hotels = _hotelRepository.GetAllHotels();
            if (hotels == null || !hotels.Any())
                return new List<HotelToReturnDto>(); 

          
            List<HotelToReturnDto> hotelsDto = hotels.Select(hotel => new HotelToReturnDto
            {
                Name = hotel.Name,
                Location = hotel.Location,
                ImagePath = hotel.ImagePath,
                Rooms = hotel.Rooms.Select(room => new RoomToReturnDto
                {
                    RoomType = room.RoomType.ToString(),
                    PricePerNight = room.PricePerNight,
                    ImagePaths = room.ImagePaths
                }).ToList()
            }).ToList();

            return hotelsDto;
        }

    }
}
