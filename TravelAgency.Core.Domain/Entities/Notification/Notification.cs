using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities._Common;

namespace TravelAgency.Core.Domain.Entities.Notification
{
    public class Notification : Entity
    {      
        public int? UserId { get; set; }
        public string? Recipient { get; set; } 
        public string? Content { get; set; }
       
        public  TemplateName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SentAt { get; set; }
        public bool IsSent { get; set; }
        public Channels? Channel { get; set; } 
        public string? FailureReason { get; set; } 
    }
}
