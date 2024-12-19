using TravelAgency.Core.Domain.Data_Storage_Initializer_Contract;

namespace TravelAgency.APIs.Extensions
{
    public static class DataInitializerExtension
    {
        public static WebApplication InitializeData(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;

            var dataInitializer = services.GetRequiredService<IDataStorageInitializer>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();


            try
            {
                 dataInitializer.Seed();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Exception has been occured While Pending The Migrations or seeding Data");
            }

            return app;
        }
    }
}
