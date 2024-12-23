using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty.Events.BLL.DTOs;
using TravelAgency.Core.Domain.Entities.Hotel_Reservation;

namespace TravelAgency.Infrastructure.ThirdParty.Services
{
    public interface IEventAdapterService
    {
        public List<EventToReturnDto> GetAllEvents(string? token);

        public List<EventToReturnDto> RecommendEvents(string? token);
        public List<EventToReturnDto> RecommendEvents(Location location, string startDate, string endDate);
        public List<EventReservationToReturnDto> GetUserReservations(string? token);
        public string ReserveEvent(string? token, int eventId);

        public string CancelReservation(string? token, int reservationId);

    }
}
