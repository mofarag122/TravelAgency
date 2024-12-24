using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Notification;

namespace TravelAgency.Core.Application.Builder.Notification_Builder
{
    public interface INotificationContentBuilder
    {
        public string BuildContent(NotificationTemplate template, Dictionary<string, string>? placeholders);
    }
}
