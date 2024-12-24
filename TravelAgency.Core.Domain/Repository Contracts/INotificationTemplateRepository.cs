using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Notification;

namespace TravelAgency.Core.Domain.Repository_Contracts
{
    public interface INotificationTemplateRepository 
    {
        public void AddTemplate(NotificationTemplate notificationTemplate);
        public void UpdateTemplate(NotificationTemplate notificationTemplate);
        public void DeleteTemplate(int id);
        public List<NotificationTemplate> GetAllTemplates();
        public NotificationTemplate GetNotificationByType(Templates template);
            
    }
}
