using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Notification;

namespace TravelAgency.Core.Domain.Repository_Contracts
{
    public interface INotificationStatisticsRepository
    {
        public int NumberOfSuccessfulEmailNotifications();
        public int NumberOfSuccessfulSMSNotifications();
        Dictionary<string, int> NumberOfFailedNotificationsWithReasons();
        public string MostSentEmail();
        public string MostSentSMS();
        public string MostSentNotificationTemplate();

    }
}
