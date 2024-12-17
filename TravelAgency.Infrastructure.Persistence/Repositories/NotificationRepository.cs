using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Entities.Notification;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Data_Storage;

namespace TravelAgency.Infrastructure.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private _StorageManagement<Notification> StorageManagement;

        private const string FilePath = "D:\\SDA Project\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\Notifications.json";

        public NotificationRepository()
        {
            StorageManagement = new _StorageManagement<Notification>(FilePath);
        }


        public async Task AddNotificationAsync(Notification notification)
        {
            StorageManagement.Add(notification);
            await Task.CompletedTask;
        }

        public IEnumerable<Notification> GetAllNotifications()
        {
            return StorageManagement.GetAll();
        }

        public async Task UpdateNotification(Notification notification)
        {
            
            StorageManagement.Update(notification);
            await Task.CompletedTask;
        }
    }
}
