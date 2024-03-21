using EventBooking_BE.Data;
using EventBooking_BE.Models;
using EventBooking_BE.ServiceAbstraction;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EventBooking_BE.Services
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity?> FindById<TEntity>(string id) where TEntity : Entity
        {
            return await _context.Set<TEntity>().FindAsync(id) ?? null;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
