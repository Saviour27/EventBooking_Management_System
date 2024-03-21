using EventBooking_BE.Data;
using EventBooking_BE.Models;
using EventBooking_BE.ServiceAbstraction;
using Microsoft.EntityFrameworkCore;

namespace EventBooking_BE.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            UpdateAuditableEntities();
            return await _context.SaveChangesAsync();
        }

        private void UpdateAuditableEntities()
        {
            var entries = _context.ChangeTracker.Entries<IAuditable>();

            foreach (var entry in entries)
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(e => e.CreatedAt).CurrentValue = DateTimeOffset.UtcNow;
                        entry.Property(e => e.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Property(e => e.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;
                        break;
                }
        }
    }
}
