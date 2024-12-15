using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Domain.Entities.Notification;
using TravelAgency.Core.Domain.Repository_Contracts;

public class NotificationService : INotificationService
{
    private readonly ConcurrentQueue<Notification> _notificationQueue = new ConcurrentQueue<Notification>();
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(INotificationRepository notificationRepository, ILogger<NotificationService> logger)
    {
        _notificationRepository = notificationRepository;
        _logger = logger;
    }

    public Task QueueNotificationAsync(Notification notification)
    {
        _notificationQueue.Enqueue(notification);
        _logger.LogInformation($"Notification queued: {notification.Id}");
        return Task.CompletedTask;
    }

    public async Task ProcessQueueAsync(CancellationToken cancellationToken = default)
    {
        while (!_notificationQueue.IsEmpty && !cancellationToken.IsCancellationRequested)
        {
            if (_notificationQueue.TryDequeue(out var notification))
            {
                try
                {
                    await SendNotificationAsync(notification);
                    notification.SentAt = DateTime.UtcNow;
                    notification.IsSent = true;

                     await _notificationRepository.AddNotificationAsync(notification);
                    _logger.LogInformation($"Notification sent and saved: {notification.Id}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to process notification: {notification.Id}");
                    notification.IsSent=false;
                    await _notificationRepository.AddNotificationAsync(notification);
                    notification.FailureReason = ex.Message;    
                }
            }
        }
    }

    private Task SendNotificationAsync(Notification notification)
    {
    
        _logger.LogInformation($"Sending notification to {notification.Recipient}");
        return Task.Delay(100); 
    }

  
}
