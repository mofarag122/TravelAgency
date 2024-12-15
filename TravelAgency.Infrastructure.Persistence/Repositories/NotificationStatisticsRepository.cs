using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Notification;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Data_Storage;

namespace TravelAgency.Infrastructure.Persistence.Repositories
{
    public class NotificationStatisticsRepository : INotificationStatisticsRepository
    {
        private _StorageManagement<Notification> StorageManagement;

        private const string FilePath = "C:\\Users\\Asus\\Source\\Repos\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\Notifications.json";

        public NotificationStatisticsRepository()
        {
            StorageManagement = new _StorageManagement<Notification>(FilePath);
        }


        public string MostSentEmail()
        {
            var mostSentEmail = StorageManagement.GetAll()
                                                 .Where(n => n.Channel == Channels.email && n.IsSent == true)
                                                 .GroupBy(n => n.Recipient)
                                                 .OrderByDescending(group => group.Count())
                                                 .FirstOrDefault();

            return mostSentEmail?.Key ?? "No emails sent";
        }

        public string MostSentNotificationTemplate()
        {
           
                var mostSentTemplate = StorageManagement.GetAll()
                    .Where(n => n.IsSent == true)
                    .GroupBy(n => n.TemplateName)
                    .OrderByDescending(group => group.Count())
                    .FirstOrDefault();

                return mostSentTemplate?.Key.ToString() ?? "No templates sent";  
            
        }

        public string MostSentSMS()
        {
            var mostSentSMS = StorageManagement.GetAll()
                                               .Where(n => n.Channel == Channels.sms && n.IsSent == true)
                                               .GroupBy(n => n.Recipient)
                                               .OrderByDescending(group => group.Count())
                                               .FirstOrDefault();

            return mostSentSMS?.Key ?? "No SMS sent";
        }

        public int NumberOfSuccessfulEmailNotifications()
        {
            return StorageManagement.GetAll().Where(n => n.Channel == Channels.email && n.IsSent == true).Count();
        }

        public int NumberOfSuccessfulSMSNotifications()
        {
            return StorageManagement.GetAll().Where(n => n.Channel == Channels.sms && n.IsSent == true).Count();
        }

        public Dictionary<string, int> NumberOfFailedNotificationsWithReasons()
        {
            return StorageManagement.GetAll()
                                    .Where(n => n.IsSent == false)
                                    .GroupBy(n => n.FailureReason!)
                                    .ToDictionary(group => group.Key, group => group.Count());
        }

    }
}
