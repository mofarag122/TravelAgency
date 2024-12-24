using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Notification;

namespace TravelAgency.Core.Application.Builder.Notification_Builder
{
    public class NotificationContentBuilder : INotificationContentBuilder
    {
        public string BuildContent(NotificationTemplate template, Dictionary<string, string>? placeholders)
        {
            string content = template.TemplateContent;

           
            foreach (var placeholder in placeholders!)
            {
                content = content.Replace($"{{{placeholder.Key}}}", placeholder.Value);
            }

            return content;
        }
    }

}
