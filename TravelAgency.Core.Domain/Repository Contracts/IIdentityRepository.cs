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
        public bool AddUser(User User);

        public void UpdateUser(User user);
        public User FindUserByEmail(string Email);

        public User FindUserByUserName(string UserName);
        public User FindUserByPhoneNumber(string UserName);

        public User FindUserById(int userId);
        

        public bool CheckEmailWithPassword(string Email, string HashedPassword);
    }
}
