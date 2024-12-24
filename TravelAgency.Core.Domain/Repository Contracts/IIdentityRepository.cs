using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities.Identity;

namespace TravelAgency.Core.Domain.Repository_Contracts
{
    public interface IIdentityRepository
    {
        public bool AddUser(User user);
        public void UpdateUser(User user);
        public User FindUserByEmail(string email);
        public User FindUserByUserName(string userName);
        public User FindUserByPhoneNumber(string userName);
        public User FindUserById(int userId);
        public bool CheckEmailWithPassword(string email, string ashedPassword);
    }
}
