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

        public HotelReservationService(IHotelRepository hotelRepository, IHotelReservationRepository reservationRepository)
        {
            _hotelRepository = hotelRepository;
            _reservationRepository = reservationRepository;
        }

        public List<HotelToReturnDto> GetAllHotels()
        {
            List<Hotel> hotels = _hotelRepository.GetAllHotels();

            if (hotels is null)
                return null!;

            List<HotelToReturnDto> hotelsDto = new List<HotelToReturnDto>();
            foreach (var hotel in hotels)
            {
                HotelToReturnDto hotelDto = new HotelToReturnDto();
                hotelDto.Name = hotel.Name;
                hotelDto.Location = hotel.Location;
                hotelDto.ImagePath = hotel.ImagePath;
                List<RoomToReturnDto> roomsDto = new List<RoomToReturnDto>();
                RoomToReturnDto roomDto = new RoomToReturnDto();
                foreach (var room in hotel.Rooms)
                {

                    roomDto.RoomType = room.RoomType.ToString();
                    roomDto.PricePerNight = room.PricePerNight;
                    roomDto.ImagePaths = room.ImagePaths;

                    roomsDto.Add(roomDto);

                }
                hotelDto.Rooms = roomsDto;

                hotelsDto.Add(hotelDto);
            }
            return hotelsDto;
        }
    }
}