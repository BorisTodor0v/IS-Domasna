using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain.DomainModels;
using Tickets.Repository.Interface;

namespace Tickets.Repository.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Ticket> tickets;
        public TicketRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.tickets = context.Set<Ticket>();
        }

        public Ticket Get(Guid? Id)
        {
            var result = tickets
                .Include(x => x.Category)
                .FirstOrDefault(x => x.ID  == Id);
            return result;
        }

        public List<Ticket> GetAll()
        {
            List<Ticket> result = tickets
                .Include (x => x.Category)
                .ToList();
            return result;
        }

        public List<Ticket> GetByCategory(Guid? categoryId)
        {
            List<Ticket> result = tickets
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync().Result;
            return result;
        }

        public List<Ticket> GetByDate(DateTime? date)
        {
            List<Ticket> result = tickets
                .Where(x => x.date.Equals(date))
                .ToListAsync().Result;
            return result;
        }
    }
}
