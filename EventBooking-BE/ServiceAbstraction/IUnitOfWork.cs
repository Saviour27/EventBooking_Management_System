namespace EventBooking_BE.ServiceAbstraction
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync();
    }
}
