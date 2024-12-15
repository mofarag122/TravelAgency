
using TravelAgency.APIs.Middlewares;
using TravelAgency.Core.Application.Service_Contracts;
using TravelAgency.Core.Application.Services.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Repositories;

namespace TravelAgency.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped(typeof(IIdentityRepository), typeof(IdentityRepository));
            builder.Services.AddScoped(typeof(IIdentityService), typeof(IdentityService));

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
