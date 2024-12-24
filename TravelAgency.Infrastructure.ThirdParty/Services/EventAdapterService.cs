using ThirdParty.Events.BLL.Services;
using ThirdParty.Events.BLL.DTOs;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Application.Exceptions;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Core.Domain.Entities.Identity;
using ThirdParty.Events.Presistense.Entities;
using TravelAgency.Core.Application.Builder.Notification_Builder;
using TravelAgency.Core.Domain.Entities.Notification;



namespace TravelAgency.Infrastructure.ThirdParty.Services
{
    public class EventAdapterService : IEventAdapterService
    {
        private IEventService _eventService;
        private IAuthenticationRepository _authenticationRepository;
        private IHotelReservationRepository _hotelReservationRepository;
        private IHotelRepository _hotelRepository;  

        public EventAdapterService(IEventService eventService , IAuthenticationRepository authenticationRepository ,  IHotelReservationRepository hotelReservationRepository , IHotelRepository hotelRepository)
        {
            _eventService = eventService;
            _authenticationRepository = authenticationRepository;
            _hotelReservationRepository = hotelReservationRepository;
            _hotelRepository = hotelRepository;
        }
        public List<EventToReturnDto> GetAllEvents(string? token)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            return _eventService.GetAllEvents();
        }
        public List<EventToReturnDto> RecommendEvents(string? token)
        {

            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            List<HotelReservation> userHotelReservations =  _hotelReservationRepository.GetUserReservations(authentication.UserId)!;
           
            List<EventToReturnDto> recommendedEventsDto = new List<EventToReturnDto>();

          
            foreach (var userReservation in userHotelReservations)
            {
                if(userReservation.StartDate >= DateOnly.FromDateTime(DateTime.UtcNow))
                {
                    Hotel hotel = _hotelRepository.GetHotel(userReservation.HotelId);
                    EventLocation hotelLocation = new EventLocation()
                    {
                        Country = hotel.Location.Country,
                        City = hotel.Location.City,
                        Region = hotel.Location.Region,
                    };
                    List<EventToReturnDto> eventsDto = _eventService.GetEventsByLocation(hotelLocation);
                    foreach (var eventDto in eventsDto)
                    {

                        if (DateOnly.TryParse(eventDto.StartDate, out var eventStartDate) &&
                            DateOnly.TryParse(eventDto.EndDate, out var eventEndDate))
                        {
                            if (eventStartDate >= userReservation.StartDate && eventEndDate <= userReservation.EndDate)
                            {
                                recommendedEventsDto.Add(eventDto);
                            }
                        }

                    }

                }
            }
            return recommendedEventsDto;    

        }
        public List<EventToReturnDto> RecommendEvents(Location location, string startDate, string endDate)
        {

            EventLocation eventLocation = new EventLocation()
            {
                Country = location.Country,
                City = location.City,
                Region = location.Region,
            };

        
            
            if (!DateOnly.TryParse(startDate, out var parsedStartDate))
            {
                throw new ArgumentException("Invalid start date format", nameof(startDate));
            }
            if (!DateOnly.TryParse(endDate, out var parsedEndDate))
            {
                throw new ArgumentException("Invalid end date format", nameof(endDate));
            }

            List<EventToReturnDto> eventsDto = _eventService.GetEventsByLocation(eventLocation);
            List<EventToReturnDto> recommendedEventsDto = new List<EventToReturnDto>();

            foreach (var eventDto in eventsDto)
            {
                
                if (DateOnly.TryParse(eventDto.StartDate, out var eventStartDate) &&
                    DateOnly.TryParse(eventDto.EndDate, out var eventEndDate))
                {
                    if (eventStartDate >= parsedStartDate && eventEndDate <= parsedEndDate)
                    {
                        recommendedEventsDto.Add(eventDto);
                    }
                }
            }

            return recommendedEventsDto;
        }
        public List<EventReservationToReturnDto> GetUserReservations(string? token)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            return _eventService.GetUserReservations(authentication.UserId);
        }
        public string ReserveEvent(string? token , int eventId ,INotificationRepository notificationRepository, INotificationTemplateRepository notificationTemplateRepository, INotificationContentBuilder notificationContentBuilder , IIdentityRepository identityRepository)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            User user = identityRepository.FindUserById(authentication.UserId);

          
                try
                {
                    if (_eventService.ReserveEvent(eventId, authentication.UserId))
                    {
                        NotificationTemplate notificationTemplate = notificationTemplateRepository.GetNotificationByType(Templates.eventReservation);
                        Dictionary<string, string> placeholders = new Dictionary<string, string>()
                        {
                            {"UserName",user.UserName},
                        };
                        string content = notificationContentBuilder.BuildContent(notificationTemplate, placeholders);
                        Notification notification = new Notification()
                        {
                            UserId = user.Id,
                            Recipient = user.PhoneNumber ?? user.Email,
                            Content = content,
                            TemplateName = Templates.hotelReservation,
                            Channel = user.PhoneNumber != null ? Channels.sms : Channels.email,
                        };
                        notificationRepository.AddNotificationAsync(notification);

                        return "Reservation Done Successfully.";
                    }
                    else
                         return "Event Expired.";
                }
                catch (Exception ex)
                {

                    throw new BadRequest(ex.Message);
                }
             
        }
            
        public string CancelReservation(string? token, int reservationId)
        {
            if (token is null)
                throw new UnAutherized("You Are Not Autherized.");

            Authentication authentication = _authenticationRepository.FindUserByToken(token) ?? null!;

            if (authentication is null)
                throw new UnAutherized("You Are Not Autherized.");

            return _eventService.CancelReservation(reservationId);
        }
    }
}
