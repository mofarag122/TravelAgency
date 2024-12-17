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
using TravelAgency.Core.Domain.Entities.Notification;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Core.Application.Services
{
    public class HotelReservationService : IHotelReservationService
    {
        private IHotelRepository _hotelRepository;
        private IHotelReservationRepository _reservationRepository;
        private IAuthenticationRepository _authenticationRepository;
        private INotificationRepository _notificationRepository;
        private IIdentityRepository _identityRepository;    
        public HotelReservationService(IHotelRepository hotelRepository, IHotelReservationRepository reservationRepository , IAuthenticationRepository authenticationRepository, INotificationRepository notificationRepository , IIdentityRepository identityRepository)
        {
            _hotelRepository = hotelRepository;
            _reservationRepository = reservationRepository;
            _authenticationRepository = authenticationRepository;
            _notificationRepository = notificationRepository;
            _identityRepository = identityRepository;
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

        public string ReserveRoom(string? token, ReservationToCreateDto reservationDto)
        {
       
            if (token is null)
                throw new UnAutherized("You Are Not Authorized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token)!;
            if (authentication is null)
                throw new UnAutherized("You Are Not Authorized.");

            if (DateOnly.Parse(reservationDto.StartDate) > DateOnly.Parse(reservationDto.EndDate))
                throw new BadRequest("Start Date Must be Less Than End Date");

            if (DateTime.Parse(reservationDto.StartDate) < DateTime.UtcNow)
                throw new BadRequest($"Start Date Must be from {DateTime.UtcNow}");
           
            List<HotelReservation?> roomReservations = _reservationRepository.GetReservations(reservationDto.HotelId, reservationDto.RoomId);

            if (roomReservations != null)
            {
                foreach (var roomReservation in roomReservations)
                {
                    
                    if (DateOnly.TryParse(reservationDto.StartDate, out DateOnly startDate) &&
                        DateOnly.TryParse(reservationDto.EndDate, out DateOnly endDate))
                    {
                        if (startDate <= roomReservation!.EndDate)
                        {
                            return $"Room is reserved from {roomReservation.StartDate.ToString("yyyy-MM-dd")} to {roomReservation.EndDate.ToString("yyyy-MM-dd")}";
                        }
                    }
                }
                
            }

           
            User? user = _identityRepository.FindUserById(authentication.UserId);
            if (user is null)
                throw new Exception("User not found.");

           
            Notification notification = new Notification()
            {
                UserId = user.Id,
                Recipient = user.PhoneNumber ?? user.Email,
                Content = $"Congratulations {user.UserName}, Your Reservation Done Successfully",
                TemplateName = Templates.hotelReservation,
                Channel = user.PhoneNumber != null ? Channels.sms : Channels.email,
            };

            _notificationRepository.AddNotificationAsync(notification);

            Room room = _hotelRepository.GetRoomById(reservationDto.HotelId, reservationDto.RoomId);
            if (room is null)
                throw new Exception("Room not found.");

            
            HotelReservation hotelReservation = new HotelReservation()
            {
                UserId = user.Id,
                HotelId = reservationDto.HotelId,
                RoomsId = reservationDto.RoomId,
                StartDate = DateOnly.TryParse(reservationDto.StartDate, out DateOnly startDateResult) ? startDateResult : default,
                EndDate = DateOnly.TryParse(reservationDto.EndDate, out DateOnly endDateResult) ? endDateResult : default
            };

            
            hotelReservation.Cost = room.PricePerNight * hotelReservation.Duration;
            _reservationRepository.AddReservation(hotelReservation);
           

            return "Your Reservation Done Successfully";
        }
  

    }
}
