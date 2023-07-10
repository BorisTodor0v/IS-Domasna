using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain.DomainModels;
using Tickets.Domain.Identity;

namespace Tickets.Repository
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
                .Property(x => x.ID).ValueGeneratedOnAdd();

            builder.Entity<Category>()
                .Property(x => x.ID).ValueGeneratedOnAdd();

            builder.Entity<Ticket>()
                .HasOne<Category>(x => x.Category)
                .WithOne()
                .HasForeignKey<Ticket>(x => x.CategoryId);

            builder.Entity<Cart>()
                .Property(x => x.ID).ValueGeneratedOnAdd();

            builder.Entity<CartItem>()
                .HasOne(x => x.Ticket)
                .WithMany(x => x.CartItems)
                .HasForeignKey(x => x.CartID);

            builder.Entity<CartItem>()
                .HasOne(x => x.Cart)
                .WithMany(x => x.CartItems)
                .HasForeignKey(x => x.TicketID);

            builder.Entity<Cart>()
                .HasOne<AppUser>(x => x.User)
                .WithOne(x => x.UserCart)
                .HasForeignKey<Cart>(x => x.UserID);

            builder.Entity<OrderItem>()
                .HasOne(x => x.OrderedTicket)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderID);

            builder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.TicketID);
        }
    }
}