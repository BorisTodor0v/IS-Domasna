using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.Identity;
using Tickets.Repository.Implementation;
using Tickets.Repository.Interface;
using Tickets.Services.Interface;

namespace Tickets.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public IEnumerable<AppUser> GetAll()
        {
            return this.userRepository.GetAll();
        }

        public AppUser GetUserDetails(string userId)
        {
            return this.userRepository.Get(userId);
        }

        public void UpdateUser(AppUser user)
        {
            this.userRepository.Update(user);
        }
    }
}
