using EventBooking_BE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventBooking_BE.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User>? User { get; set; }
        public DbSet<Event>? Event { get; set; }
        public DbSet<Booking>? Booking { get; set; }
        public DbSet<UserEvent>? UserEvent { get; set; }
        public DbSet<WalletTransaction>? WalletTransactions { get; set; }
        
    }
}