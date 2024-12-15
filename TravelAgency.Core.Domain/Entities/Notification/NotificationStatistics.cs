using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core.Domain.Entities.Notification
{
    public class NotificationStatistics
    {
        public int TotalSent { get; set; }
        public int Failed { get; set; }
        public int Pending { get; set; }

        public void IncrementSent() => TotalSent++;
        public void IncrementFailed() => Failed++;
        public void IncrementPending() => Pending++;
    }
}
