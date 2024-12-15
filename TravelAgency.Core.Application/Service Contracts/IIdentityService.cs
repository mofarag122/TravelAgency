using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Application.DTOs.Identity;
using TravelAgency.Core.Application.DTOs.Notification;
using TravelAgency.Core.Domain.Entities.Identity;

namespace TravelAgency.Core.Application.Service_Contracts
{
    public interface IIdentityService
    {
        public User Register(UserToRegisterDto userDto);

        public bool Login(UserToLoginDto userDto);

        public Task<NotificationToResetPasswordDto> ResetPassword(INotificationService notificationService, UserToResetPasswordDto userDto);
    }
}
