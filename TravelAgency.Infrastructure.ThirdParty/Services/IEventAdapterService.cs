using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty.Events.BLL.DTOs;
using TravelAgency.Core.Application.Builder.Notification_Builder;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Infrastructure.ThirdParty.Services
{
    public interface IEventAdapterService
    {
        public List<EventToReturnDto> GetAllEvents(string? token);

        public List<EventToReturnDto> RecommendEvents(string? token ,IHotelRepository hotelRepository, IHotelReservationRepository reservationRepository);
        public List<EventToReturnDto> RecommendEvents(Location location, string startDate, string endDate);
        public List<EventReservationToReturnDto> GetUserReservations(string? token);
        public string ReserveEvent(string? token, int eventId,INotificationRepository notificationRepository, INotificationTemplateRepository notificationTemplateRepository, INotificationContentBuilder notificationContentBuilder, IIdentityRepository identityRepository);

        public string CancelReservation(string? token, int reservationId);

    }
}
