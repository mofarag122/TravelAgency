using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Repositories;

namespace TravelAgency.AdminDashboard
{
    internal class Program
    {
        private static INotificationStatisticsRepository notificationStatisticsRepo = null!;

        static void Main(string[] args)
        {
         
            notificationStatisticsRepo = new NotificationStatisticsRepository();

            Console.WriteLine("Welcome to the Notification Statistics Dashboard");
            string choice;
            do
            {
                Console.WriteLine("\nChoose a feature from the menu:");
                Console.WriteLine("1. Number of successfully sent email and SMS notifications.");
                Console.WriteLine("2. Number of unsuccessfully sent notifications with reasons.");
                Console.WriteLine("3. The most notified email address and phone number.");
                Console.WriteLine("4. The most sent notification template.");
                Console.WriteLine("5. Exit.");
                Console.Write("Enter your choice: ");
                
                choice = Console.ReadLine() ?? "5";


                switch (choice)
                {
                    case "1":
                        DisplaySuccessfulNotifications();
                        break;

                    case "2":
                        DisplayFailedNotificationsWithReasons();
                        break;

                    case "3":
                        DisplayMostNotifiedContacts();
                        break;

                    case "4":
                        DisplayMostSentTemplate();
                        break;

                    case "5":
                        Console.WriteLine("Exiting the dashboard. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine("------------------------------------------------------------------------------------------------");

            } while (choice != "5");
        }

        private static void DisplaySuccessfulNotifications()
        {
            int emailCount = notificationStatisticsRepo.NumberOfSuccessfulEmailNotifications();
            int smsCount = notificationStatisticsRepo.NumberOfSuccessfulSMSNotifications();

            Console.WriteLine($"\nNumber of successfully sent email notifications: {emailCount}");
            Console.WriteLine($"Number of successfully sent SMS notifications: {smsCount}");
        }

        private static void DisplayFailedNotificationsWithReasons()
        {
          
            Dictionary<string , int> failureReasons = notificationStatisticsRepo.NumberOfFailedNotificationsWithReasons();

            if (failureReasons.Count == 0)
            {
                Console.WriteLine("\nNo failed notifications found.");
            }
            else
            {
                Console.WriteLine("\nFailed notifications with reasons:");
                foreach (var reason in failureReasons)
                {
                    Console.WriteLine($"- Reason: {reason.Key ?? "Unknown"}, Count: {reason.Value}");
                }
            }
        }

        private static void DisplayMostNotifiedContacts()
        {
            
            string mostSentEmail = notificationStatisticsRepo.MostSentEmail();
            string mostSentSMS = notificationStatisticsRepo.MostSentSMS();

            Console.WriteLine($"\nThe most notified email address: {mostSentEmail}");
            Console.WriteLine($"The most notified phone number: {mostSentSMS}");
        }

        private static void DisplayMostSentTemplate()
        {
            
            string mostSentTemplate = notificationStatisticsRepo.MostSentNotificationTemplate();

            Console.WriteLine($"\nThe most sent notification template: {mostSentTemplate}");
        }
    }
}
