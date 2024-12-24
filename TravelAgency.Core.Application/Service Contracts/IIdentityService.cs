using TravelAgency.Core.Application.Builder.Notification_Builder;
using TravelAgency.Core.Application.DTOs.Identity;
using TravelAgency.Core.Application.DTOs.Notification;
using TravelAgency.Core.Domain.Entities.Identity;
using TravelAgency.Core.Domain.Repository_Contracts;

namespace TravelAgency.Core.Application.Service_Contracts
{
    public interface IIdentityService
    {
        public User Register(UserToRegisterDto userDto);
        public string Login(UserToLoginDto userDto);
        public Task<NotificationToResetPasswordDto> ResetPassword(UserToResetPasswordDto userDto, INotificationRepository notificationRepository, INotificationTemplateRepository notificationTemplateRepository, INotificationContentBuilder notificationContentBuilder);
        public string Logout(string? token);
    }
}
