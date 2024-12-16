using TravelAgency.Core.Application.DTOs.Identity;
using TravelAgency.Core.Application.DTOs.Notification;
using TravelAgency.Core.Domain.Entities.Identity;

namespace TravelAgency.Core.Application.Service_Contracts
{
    public interface IIdentityService
    {
        public User Register(UserToRegisterDto userDto);

        public string Login(UserToLoginDto userDto);

        public Task<NotificationToResetPasswordDto> ResetPassword(UserToResetPasswordDto userDto);

        public string Logout(string? token);
    }
}
