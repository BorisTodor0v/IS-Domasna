using System.ComponentModel.DataAnnotations;

namespace Tickets.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
