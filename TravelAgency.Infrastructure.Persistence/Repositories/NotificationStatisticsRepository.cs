using Microsoft.Extensions.Configuration;
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
        private _StorageManagement<Notification> _StorageManagement;
        public NotificationStatisticsRepository(IConfiguration configuration)
        {
            string filePath = configuration["FileStorage:NotificationsFilePath"]!;
            _StorageManagement = new _StorageManagement<Notification>(filePath);
        }


        public string MostSentEmail()
        {
            var mostSentEmail = _StorageManagement.GetAll()
                                                 .Where(n => n.Channel == Channels.email && n.IsSent == true)
                                                 .GroupBy(n => n.Recipient)
                                                 .OrderByDescending(group => group.Count())
                                                 .FirstOrDefault();

            return mostSentEmail?.Key ?? "No emails sent";
        }
        public string MostSentNotificationTemplate()
        {
           
                var mostSentTemplate = _StorageManagement.GetAll()
                                                        .Where(n => n.IsSent == true)
                                                        .GroupBy(n => n.TemplateName)
                                                        .OrderByDescending(group => group.Count())
                                                        .FirstOrDefault();

                return mostSentTemplate?.Key.ToString() ?? "No templates sent";  
            
        }
        public string MostSentSMS()
        {
            var mostSentSMS = _StorageManagement.GetAll()
                                               .Where(n => n.Channel == Channels.sms && n.IsSent == true)
                                               .GroupBy(n => n.Recipient)
                                               .OrderByDescending(group => group.Count())
                                               .FirstOrDefault();

            return mostSentSMS?.Key ?? "No SMS sent";
        }
        public int NumberOfSuccessfulEmailNotifications()
        {
            return _StorageManagement.GetAll().Where(n => n.Channel == Channels.email && n.IsSent == true).Count();
        }
        public int NumberOfSuccessfulSMSNotifications()
        {
            return _StorageManagement.GetAll().Where(n => n.Channel == Channels.sms && n.IsSent == true).Count();
        }
        public Dictionary<string, int> NumberOfFailedNotificationsWithReasons()
        {
            return _StorageManagement.GetAll()
                                    .Where(n => n.IsSent == false)
                                    .GroupBy(n => n.FailureReason!)
                                    .ToDictionary(group => group.Key, group => group.Count());
        }

    }
}
