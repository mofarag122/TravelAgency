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
    public class NotificationTemplateRepository : INotificationTemplateRepository
    {
        private _StorageManagement<NotificationTemplate> _StorageManagement;
        public NotificationTemplateRepository(IConfiguration configuration)
        {
            string filePath = configuration["FileStorage:NotificationTemplatesFilePath"]!;
            _StorageManagement = new _StorageManagement<NotificationTemplate>(filePath);
        }

        public void AddTemplate(NotificationTemplate notificationTemplate)
        {
            _StorageManagement.Add(notificationTemplate);
        }
        public void UpdateTemplate(NotificationTemplate notificationTemplate)
        {
            _StorageManagement.Update(notificationTemplate);   
        }
        public void DeleteTemplate(int id)
        {
           _StorageManagement.Delete(id);
        }
        public NotificationTemplate GetNotificationByType(Templates template)
        {
            return _StorageManagement.GetAll().Where(t => t.Template == template).Last();
        }
        public List<NotificationTemplate> GetAllTemplates()
        {
            return _StorageManagement.GetAll().ToList();
        }
    }
}
