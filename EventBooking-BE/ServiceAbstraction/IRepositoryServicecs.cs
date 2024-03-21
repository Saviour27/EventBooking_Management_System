using EventBooking_BE.Data;
using EventBooking_BE.Models;
using System.Linq.Expressions;

namespace EventBooking_BE.ServiceAbstraction
{
    public interface IRepository
    {
        public Task Add<TEntity>(TEntity entity) where TEntity : Entity;
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity;
        public Task<TEntity?> FindById<TEntity>(string id) where TEntity : Entity;
        public void Update<TEntity>(TEntity entity) where TEntity : Entity;
        public void Remove<TEntity>(TEntity entity) where TEntity : Entity;
    }
}


