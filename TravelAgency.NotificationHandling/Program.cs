using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Core.Domain.Entities.Notification;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.NotificationHandling
{
    internal class Program
    {
        private static readonly Queue<Notification> NotificationQueue = new();

        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            var services = new ServiceCollection();
            services.AddNotificationHandlingServices(configuration); 
            var serviceProvider = services.BuildServiceProvider();

            var notificationRepository = serviceProvider.GetService<INotificationRepository>();
            if (notificationRepository == null) throw new Exception("NotificationRepository not found");

            var cancellationTokenSource = new CancellationTokenSource();

          
            Console.WriteLine("Starting Notification Processor...");
            await Task.Run(() => ProcessNotifications(notificationRepository, cancellationTokenSource.Token));

            Console.WriteLine("Notification Processor stopped.");
        }

        static async Task ProcessNotifications(INotificationRepository notificationRepository, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (NotificationQueue.Count == 0)
                {
                    Console.WriteLine("[Queue Empty] Fetching new notifications...");
                    var notifications = notificationRepository.GetAllNotifications();
                    foreach (var notification in notifications.Where(n => !n.IsSent))
                    {
                        NotificationQueue.Enqueue(notification);
                    }

                    if (NotificationQueue.Count == 0)
                    {
                        Console.WriteLine("[No Pending Notifications] Waiting for 2 seconds...");
                        await Task.Delay(2000, cancellationToken);
                        continue;
                    }
                }

                while (NotificationQueue.Count > 0)
                {
                    var notification = NotificationQueue.Dequeue();
                    try
                    {
                        Console.WriteLine($"[Sending] Content: {notification.Content}");
                        await Task.Delay(1000, cancellationToken); 

                    
                        notification.IsSent = true;
                        notification.SentAt = DateTime.UtcNow;
                        await notificationRepository.UpdateNotification(notification);

                        Console.WriteLine($"[Sent] Content: {notification.Content}, Channel: {notification.Channel}, Recipient: {notification.Recipient}");
                    }
                    catch (Exception ex)
                    {
                        notification.FailureReason = ex.Message;
                        Console.WriteLine($"[Error] Failed to process notification: {ex.Message}");
                    }
                }

                await Task.Delay(2000, cancellationToken); 
            }
        }
    }
}
