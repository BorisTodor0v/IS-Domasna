using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.DTO;

namespace Tickets.Services.Interface
{
    public interface ICartService
    {
        CartDto CartDetails(string userId);
        bool DeleteCartItem(string userId, Guid id);
        bool OrderCart(string userId);
    }
}
