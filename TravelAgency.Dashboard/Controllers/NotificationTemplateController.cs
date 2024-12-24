using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.Domain.Entities.Notification;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Dashboard.Controllers
{
    public class NotificationTemplateController : BaseApiController
    {
        private INotificationTemplateRepository _NotificationTemplateRepository;

        public NotificationTemplateController(INotificationTemplateRepository notificationTemplateRepository)
        {
            _NotificationTemplateRepository = notificationTemplateRepository;
        }

        [HttpPost] 
        public void AddTemplate(NotificationTemplate notificationTemplate)
        {
           _NotificationTemplateRepository.AddTemplate(notificationTemplate);
        }

        [HttpPut]
        public void UpdateTemplate(NotificationTemplate notificationTemplate) 
        {
            _NotificationTemplateRepository.UpdateTemplate(notificationTemplate);
        }

        [HttpDelete]
        public void DeleteTemplate(int id)
        {
            _NotificationTemplateRepository.DeleteTemplate(id);
        }

        [HttpGet]
        public ActionResult<List<NotificationTemplate>> GetAll()
        {
            return Ok(_NotificationTemplateRepository.GetAllTemplates());   
        }
    }
}
