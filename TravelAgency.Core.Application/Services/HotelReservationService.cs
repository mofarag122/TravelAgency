using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.HotelReservation;
using TravelAgency.Core.Application.Exceptions;
using TravelAgency.Core.Application.Mapping;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Entities.Notification;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Core.Domain.Specifications_DP;
using TravelAgency.Core.Domain.Specifications_DP.Hotel_Specs;

namespace TravelAgency.Core.Application.Services
{
    public class HotelReservationService : IHotelReservationService
    {
        private IHotelRepository _hotelRepository;
        private IHotelReservationRepository _reservationRepository;
        private IAuthenticationRepository _authenticationRepository;
        private INotificationRepository _notificationRepository;
        private IIdentityRepository _identityRepository;    

        private IConfiguration _configuration;
        public HotelReservationService(IHotelRepository hotelRepository, IHotelReservationRepository reservationRepository , IAuthenticationRepository authenticationRepository, INotificationRepository notificationRepository , IIdentityRepository identityRepository ,IConfiguration configuration)
        {
            _hotelRepository = hotelRepository;
            _reservationRepository = reservationRepository;
            _authenticationRepository = authenticationRepository;
            _notificationRepository = notificationRepository;
            _identityRepository = identityRepository;
            _configuration = configuration;
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
                Rooms = null,
            }).ToList();

            return hotelsDto;

        }

        public HotelToReturnDto GetHotel( string? token, int hotelId)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            Hotel hotel = _hotelRepository.GetHotel(hotelId);

            if (hotel is null)
                throw new NotFound("Hotel", $"Number {hotelId}");

            HotelToReturnDto hotelToReturnDto = new HotelToReturnDto
            {
                Id=hotel.Id,
                Name = hotel.Name,
                ImagePath=hotel.ImagePath,
                Location = hotel.Location,
            };

            return hotelToReturnDto;    

        }

        public List<HotelToReturnDto> GetHotels(string? token , HotelSpecParmas hotelSpecs)
        {

            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            BaseSpecifications<Hotel> spec = new HotelFilterationSpecifications(hotelSpecs.Sort, hotelSpecs.SearchedName, hotelSpecs.SearchedCountry, hotelSpecs.SearchedCity, hotelSpecs.SearchedRegion);

            List<Hotel> hotels = _hotelRepository.GetHotels(spec);
           
            if (hotels is null)
                throw new NotFound("Hotels", "These Criteria");

            List<HotelToReturnDto> hotelsDto = hotels.Select(hotel => new HotelToReturnDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Location = hotel.Location,
                ImagePath = hotel.ImagePath,
                Rooms = null,
            }).ToList();

            return hotelsDto;

        }

        public RoomToReturnDto GetRoom(string? token , int hotelId , int roomId)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

           Room room =   _hotelRepository.GetRoomById(hotelId, roomId);

            if (room is null)
                throw new NotFound("Room", $"Number {roomId}");

            return new RoomToReturnDto
            {
                Id = room.Id,
                ImagePaths = room.ImagePaths,
                RoomType = room.RoomType.ToString(),
                PricePerNight = room.PricePerNight,
            };
        }
        public List<RoomToReturnDto> GetRooms(string? token , int hotelId , RoomSpecParams roomSpecs)
        {

            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");



            BaseSpecifications<Room> spec = new RoomsFilterationSpecifation(roomSpecs.Price, roomSpecs.Type, roomSpecs.Sort);

            List<Room> rooms = _hotelRepository.GetRooms(hotelId, spec);

            if (rooms is null)
                throw new NotFound("Rooms", "These Criteria");

            List<RoomToReturnDto> roomsToReturnDto = rooms.Select(room => new RoomToReturnDto
            {
                Id = room.Id,

                RoomType = room.RoomType.ToString(),

                ImagePaths = room.ImagePaths,

                PricePerNight = room.PricePerNight,

            }).ToList();

            return roomsToReturnDto;
        }

        public bool ReserveRoom(string? token, ReservationToCreateDto reservationDto)
        {
       
            if (token is null)
                throw new UnAutherized("You Are Not Authorized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token)!;
            if (authentication is null)
                throw new UnAutherized("You Are Not Authorized.");

            if (DateTime.Parse(reservationDto.StartDate) < DateTime.UtcNow)
                throw new BadRequest($"Start Date Must be from {DateTime.UtcNow}");

            if (DateOnly.Parse(reservationDto.StartDate) > DateOnly.Parse(reservationDto.EndDate))
                throw new BadRequest("Start Date Must be Less Than End Date");

            Room room = _hotelRepository.GetRoomById(reservationDto.HotelId, reservationDto.RoomId);

            if (room is null)
                throw new NotFound("Room", $"Number {reservationDto.RoomId}");



            List<HotelReservation?> roomReservations = _reservationRepository.GetReservations(reservationDto.HotelId, reservationDto.RoomId);         
            if (roomReservations != null)
            {
                foreach (var roomReservation in roomReservations)
                {
                    
                    if (DateOnly.TryParse(reservationDto.StartDate, out DateOnly startDate) &&
                        DateOnly.TryParse(reservationDto.EndDate, out DateOnly endDate))
                    {
                        if (!(endDate < roomReservation!.StartDate || startDate > roomReservation.EndDate))
                        {
                            throw new BadRequest( $"Room is reserved from {roomReservation.StartDate.ToString("yyyy-MM-dd")} to {roomReservation.EndDate.ToString("yyyy-MM-dd")}");
                        }
                    }
                }
                
            }

           
            User? user = _identityRepository.FindUserById(authentication.UserId);      
            Notification notification = new Notification()
            {
                UserId = user.Id,
                Recipient = user.PhoneNumber ?? user.Email,
                Content = $"Congratulations {user.UserName}, Your Reservation Done Successfully",
                TemplateName = Templates.hotelReservation,
                Channel = user.PhoneNumber != null ? Channels.sms : Channels.email,
            };

            _notificationRepository.AddNotificationAsync(notification);
 
            HotelReservation hotelReservation = new HotelReservation()
            {
                UserId = user.Id,
                HotelId = reservationDto.HotelId,
                RoomId = reservationDto.RoomId,
                StartDate = DateOnly.TryParse(reservationDto.StartDate, out DateOnly startDateResult) ? startDateResult : default,
                EndDate = DateOnly.TryParse(reservationDto.EndDate, out DateOnly endDateResult) ? endDateResult : default
            };

            
            hotelReservation.Cost = room.PricePerNight * hotelReservation.Duration;
            _reservationRepository.AddReservation(hotelReservation);
           

            return true;
        }
  

    }
}
