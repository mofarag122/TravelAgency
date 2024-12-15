﻿using System;
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

        private const string FilePath = "C:\\Users\\Asus\\Source\\Repos\\TravelAgency\\TravelAgency.Infrastructure.Persistence\\Data Storage\\Notifications.json";

        public NotificationRepository()
        {
            StorageManagement = new _StorageManagement<Notification>(FilePath);
        }


        public void AddNotification(Notification notification)
        {
             StorageManagement.Add(notification);
            
        }

        public IEnumerable<Notification> GetAllNotifications()
        {
            return StorageManagement.GetAll();
        }
    }
}
