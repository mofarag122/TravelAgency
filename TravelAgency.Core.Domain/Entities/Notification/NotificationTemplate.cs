using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities._Common;

namespace TravelAgency.Core.Domain.Entities.Notification
{
    public class NotificationTemplate : Entity
    {
        public string TemplateContent { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Templates Template { get; set; } 
    }
}
