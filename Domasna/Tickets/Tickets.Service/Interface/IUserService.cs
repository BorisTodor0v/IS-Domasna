using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.Identity;

namespace Tickets.Services.Interface
{
    public interface IUserService
    {
        IEnumerable<AppUser> GetAll();
        AppUser GetUserDetails(string userId);
        void UpdateUser(AppUser user);
    }
}
