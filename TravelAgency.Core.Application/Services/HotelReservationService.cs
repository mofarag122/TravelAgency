using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.HotelReservation;
using TravelAgency.Core.Application.Exceptions;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Core.Application.Services
{
    public class HotelReservationService : IHotelReservationService
    {
        private IHotelRepository _hotelRepository;
        private IHotelReservationRepository _reservationRepository;
        private IAuthenticationRepository _authenticationRepository;

        public HotelReservationService(IHotelRepository hotelRepository, IHotelReservationRepository reservationRepository , IAuthenticationRepository authenticationRepository)
        {
            _hotelRepository = hotelRepository;
            _reservationRepository = reservationRepository;
            _authenticationRepository = authenticationRepository;
        }

        public List<HotelToReturnDto> GetAllHotels(string? token)
        {


            // To Return All Hotels We Follow The Business Rules:
            /*
             * 1. Check if User Authenticated or not
             * 2. Get The Hotels with room 
             * 3. Mapping From hotel -> hotelToReturnDto with Resolving The Image Path
             */


            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication =   _authenticationRepository.FindUserByToken(token) ?? null!;
           
            if(authentication is null)
                throw new UnAutherized("You Are Not Autherized.");


            List<Hotel> hotels = _hotelRepository.GetAllHotelsWithRooms();

            if (hotels is null)
                return null!;

            List<HotelToReturnDto> hotelsDto = hotels.Select(hotel => new HotelToReturnDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Location = hotel.Location,
                ImagePath = hotel.ImagePath,
                Rooms  =  null,
            }).ToList();

            return hotelsDto;

        }

        public List<RoomToReturnDto> GetRooms(string? token , int Hotelid)
        {

            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            List<Room> rooms = _hotelRepository.GetRoomsById(Hotelid);

            List<RoomToReturnDto> roomsToReturnDto = rooms.Select(room => new RoomToReturnDto
            {
                Id = room.Id,

                RoomType = room.RoomType.ToString(),

                ImagePaths = room.ImagePaths,

                PricePerNight = room.PricePerNight,

            }).ToList();

            return roomsToReturnDto;
        }
    }
}
