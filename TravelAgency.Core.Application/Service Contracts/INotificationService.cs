using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Notification;

namespace TravelAgency.Core.Application.Service_Contracts
{
    public interface INotificationService
    {
        public  Task QueueNotificationAsync(Notification notification);

        public  Task ProcessQueueAsync(CancellationToken cancellationToken = default);
    }
}
