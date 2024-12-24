using Microsoft.Extensions.Configuration;
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
        private _StorageManagement<Notification> _StorageManagement;
        public NotificationRepository(IConfiguration configuration)
        {
            string filePath = configuration["FileStorage:NotificationsFilePath"]!;
            _StorageManagement = new _StorageManagement<Notification>(filePath);     
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            _StorageManagement.Add(notification);
            await Task.CompletedTask;
        }
        public IEnumerable<Notification> GetAllNotifications()
        {
            return _StorageManagement.GetAll();
        }
        public async Task UpdateNotification(Notification notification)
        {

            _StorageManagement.Update(notification);
            await Task.CompletedTask;
        }
    }
}
