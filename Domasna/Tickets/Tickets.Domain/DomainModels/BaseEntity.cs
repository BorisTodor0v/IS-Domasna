using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Domain.DomainModels
{
    public class BaseEntity
    {
        [Key]
        public Guid ID { get; set; }
    }
}
