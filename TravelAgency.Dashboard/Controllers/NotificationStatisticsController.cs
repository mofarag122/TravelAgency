using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Dashboard.Controllers
{
    public class NotificationStatisticsController : BaseApiController
    {
        private INotificationStatisticsRepository _notificationStatisticsRepository;

        public NotificationStatisticsController(INotificationStatisticsRepository notificationStatisticsRepository)
        {
            _notificationStatisticsRepository = notificationStatisticsRepository;
        }

        [HttpGet("numberOfSuccessfulEmailNotifications")]
        public ActionResult<int> NumberOfSuccessfulEmailNotifications()
        {
            return (_notificationStatisticsRepository.NumberOfSuccessfulEmailNotifications());
        }
        
        [HttpGet("numberOfSuccessfulSMSNotifications")]
        public int NumberOfSuccessfulSMSNotifications()
        {
            return _notificationStatisticsRepository.NumberOfSuccessfulSMSNotifications();
        }

        [HttpGet("numberOfFailedNotificationsWithReasons")]
        public Dictionary<string, int> NumberOfFailedNotificationsWithReasons()
        {
            return _notificationStatisticsRepository.NumberOfFailedNotificationsWithReasons();
        }

        [HttpGet("mostSentSMS")]
        public string MostSentSMS()
        {
            return _notificationStatisticsRepository.MostSentSMS();
        }

        [HttpGet("mostSentEmail")]
        public string MostSentEmail()
        {
            return _notificationStatisticsRepository.MostSentEmail();
        }
    }
}
