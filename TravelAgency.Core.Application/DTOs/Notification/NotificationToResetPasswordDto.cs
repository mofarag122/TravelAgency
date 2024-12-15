using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Application.DTOs.Notification
{
    public class NotificationToResetPasswordDto
    {
        public string? Content { get; set; }

        public string? Channel { get; set; }

        public string? Recipient { get; set; }
    }
}
