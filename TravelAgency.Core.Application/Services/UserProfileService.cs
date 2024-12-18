using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.HotelReservation;
using TravelAgency.Core.Application.DTOs.UserProfile;
using TravelAgency.Core.Application.Exceptions;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Entities.Notification;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Core.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private IAuthenticationRepository _authenticationRepository;

        private IIdentityRepository _identityRepository;

        private IHotelRepository _hotelRepository;

        private IHotelReservationRepository _hotelReservationRepository;
        public UserProfileService(IAuthenticationRepository authenticationRepository , IIdentityRepository identityRepository , IHotelReservationRepository hotelReservationRepository , IHotelRepository hotelRepository)
        {
            _authenticationRepository = authenticationRepository;
            _identityRepository = identityRepository;
            _hotelReservationRepository = hotelReservationRepository;
            _hotelRepository = hotelRepository;
        }

       
        public string ChangeProfile(string? token, UserToChangeProfileDto userProfileDto)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            User user = _identityRepository.FindUserById(authentication.UserId);

            if (user.Email != userProfileDto.Email && _identityRepository.FindUserByEmail(userProfileDto.Email) is not null)
                throw new BadRequest("Email already Exists.");

            if (user.UserName != userProfileDto.UserName && _identityRepository.FindUserByEmail(userProfileDto.UserName) is not null)
                throw new BadRequest("User Name already Exists.");

            if (user.PhoneNumber is not null && user.PhoneNumber != userProfileDto.PhoneNumber && _identityRepository.FindUserByPhoneNumber(userProfileDto.PhoneNumber!) is not null)
                throw new BadRequest("Phone Number already Exists.");

            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword = passwordHasher.HashPassword(null!, userProfileDto.Password);


            user.FirstName = userProfileDto.FirstName;
            user.LastName = userProfileDto.LastName; 
            user.Email = userProfileDto.Email;
            user.UserName = userProfileDto.UserName;
            user.PhoneNumber = userProfileDto.PhoneNumber;
            user.HashedPassword = hashedPassword;
            Address address = new Address()
            {
                Country = userProfileDto.Address.Country,
                City = userProfileDto.Address.City,
            };

            user.Address = address;

            _identityRepository.UpdateUser(user);

            return $"{user.UserName}' Data has been updated Successfully";


        }
        public  List<ReservationToReturnDto> ReturnAllReservation(string? token)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            List<HotelReservation> reservations = _hotelReservationRepository.GetUserReservations(authentication.UserId)!;

            if(reservations is null)
                return null!;

            List<ReservationToReturnDto> reservationDto = reservations.Select(reservation => new ReservationToReturnDto
            {
                Id = reservation.Id,
                HotelId = reservation.HotelId,
                RoomId = reservation.RoomId,
                StartDate = reservation.StartDate.ToString("yyyy-MM-dd"), 
                EndDate = reservation.EndDate.ToString("yyyy-MM-dd"),    
                Cost = reservation.Cost,
            }).ToList();

            return reservationDto;

        }
        public string CancelReservation(string? token, int reservationId)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            HotelReservation reservation =  _hotelReservationRepository.GetReservation(reservationId)!;

            if (reservation is null)
                throw new NotFound("reservation", $"Number {reservationId}");

            if (reservation.StartDate <= DateOnly.FromDateTime(DateTime.UtcNow))
                throw new BadRequest("Cancellation Not Allowed");

            if (reservation.UserId == authentication.UserId)
                _hotelReservationRepository.DeleteReservation(reservationId);
            else
                throw new UnAutherized("You Are Not Autherized.");
          

            return "reservation has been Cancelled";

        }

        public string UpdateReservation(string? token, ReservationToChangeDto reservationDto)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            HotelReservation hotelReservation = _hotelReservationRepository.GetReservation(reservationDto.ReservationId);

            if(hotelReservation is null)
                throw new NotFound("reservation", $"Number {reservationDto.ReservationId}");


            if (DateOnly.Parse(reservationDto.StartDate) > DateOnly.Parse(reservationDto.EndDate))
                throw new BadRequest("Start Date Must be Less Than End Date");


            if (hotelReservation.StartDate <= DateOnly.FromDateTime(DateTime.UtcNow))
                throw new BadRequest("Update is Not Allowed.");

            if (DateOnly.Parse(reservationDto.StartDate) <= DateOnly.FromDateTime(DateTime.UtcNow))
                throw new BadRequest($"Start Date Must be from {DateOnly.FromDateTime(DateTime.UtcNow)}");


            //List<HotelReservation?> roomReservations = _hotelReservationRepository.GetReservations(hotelReservation.HotelId, hotelReservation.RoomId);
            List<HotelReservation?> roomReservations = _hotelReservationRepository.GetReservations(hotelReservation.HotelId, hotelReservation.RoomId)
                                                                                         .Where(r => r!.Id != reservationDto.ReservationId) // Skip current reservation
                                                                                         .ToList();
            if (roomReservations != null)
            {
                foreach (var roomReservation in roomReservations)
                {
                   

                    if (DateOnly.TryParse(reservationDto.StartDate, out DateOnly startDate) &&
                        DateOnly.TryParse(reservationDto.EndDate, out DateOnly endDate))
                    {
                        if (!(endDate < roomReservation!.StartDate || startDate > roomReservation.EndDate))
                        {
                            return $"Room is reserved from {roomReservation.StartDate.ToString("yyyy-MM-dd")} to {roomReservation.EndDate.ToString("yyyy-MM-dd")}";
                        }
                    }
                }

            }
            
            Room room = _hotelRepository.GetRoomById(hotelReservation.HotelId , hotelReservation.RoomId);
            hotelReservation.StartDate = DateOnly.TryParse(reservationDto.StartDate, out DateOnly startDateResult) ? startDateResult : default;
            hotelReservation.EndDate = DateOnly.TryParse(reservationDto.EndDate, out DateOnly endDateResult) ? endDateResult : default;
            hotelReservation.Cost = room.PricePerNight * hotelReservation.Duration;
            _hotelReservationRepository.UpdateReservation(hotelReservation);
            return "reservation has been Updated";

        }
    }
}
