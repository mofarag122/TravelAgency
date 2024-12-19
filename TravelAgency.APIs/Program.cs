
using System.Text.Json;
using TravelAgency.APIs.Extensions;
using TravelAgency.APIs.Middlewares;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Application.Services;
using TravelAgency.Core.Domain.Data_Storage_Initializer_Contract;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Data_seeding;
using TravelAgency.Infrastructure.Persistence.Repositories;

namespace TravelAgency.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services & DI


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var configuration = builder.Configuration;
            builder.Services.AddSingleton<IConfiguration>(configuration);


            #region Identity Dependency Injection
            builder.Services.AddScoped(typeof(IIdentityRepository), typeof(IdentityRepository));
            builder.Services.AddScoped(typeof(IIdentityService), typeof(IdentityService));
            builder.Services.AddScoped(typeof(IAuthenticationRepository) , typeof(AuthenticationRepository));
            #endregion

            #region Notification Dependency Injection
            builder.Services.AddScoped(typeof(INotificationRepository), typeof(NotificationRepository));
            #endregion

            #region Hotel Reservation Dependency Injection
            builder.Services.AddScoped(typeof(IHotelRepository), typeof(HotelRepository));
            builder.Services.AddScoped(typeof(IHotelReservationRepository), typeof(HotelReservationRepository));
            builder.Services.AddScoped(typeof(IHotelReservationService), typeof(HotelReservationService));
            #endregion

            #region User Profile Dependency Injection

            builder.Services.AddScoped(typeof(IUserProfileService), typeof(UserProfileService));

            #endregion

            builder.Services.AddScoped(typeof(IDataStorageInitializer), typeof(DataStorageInitializer));

            builder.Services.AddDirectoryBrowser();
            #endregion


            var app = builder.Build();

            #region Seeding Data
            
            app.InitializeData();   

            #endregion
            #region Kestrel Pipelines Within a Request

            app.UseMiddleware<ExceptionHandlingMiddleware>();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
           
            #endregion

            app.Run();
        }
    }
}
