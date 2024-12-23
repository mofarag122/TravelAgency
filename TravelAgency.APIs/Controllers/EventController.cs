using Microsoft.AspNetCore.Mvc;
using ThirdParty.Events.BLL.DTOs;
using TravelAgency.Infrastructure.ThirdParty.Services;

namespace TravelAgency.APIs.Controllers
{
    public class EventController : BaseApiController
    {
        private IEventAdapterService _eventAdapterService;

        public EventController(IEventAdapterService eventAdapterService)
        {
            _eventAdapterService = eventAdapterService;
        }

        [HttpGet("getEvents")]
        public  ActionResult<List<EventToReturnDto>> GetEvents(string? token)
        {
            return _eventAdapterService.GetAllEvents(token);
        }
        [HttpPost("reserveEvent")]
        public ActionResult<string> Reserve(string? token , int eventId)
        {
            return _eventAdapterService.ReserveEvent(token, eventId);
        }
    }
}
