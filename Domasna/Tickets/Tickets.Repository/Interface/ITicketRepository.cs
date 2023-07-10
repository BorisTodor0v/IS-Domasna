using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.DomainModels;

namespace Tickets.Repository.Interface
{
    public interface ITicketRepository
    {
        List<Ticket> GetByCategory(Guid? categoryId);
        List<Ticket> GetByDate(DateTime? date);
        List<Ticket> GetAll();
        Ticket Get(Guid? Id);
    }
}
