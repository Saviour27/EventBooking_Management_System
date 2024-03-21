using EventBooking_BE.Dtos;

namespace EventBooking_BE.ServiceAbstraction
{
    public interface IPaystackService
    {
        Task<Result<string>> InitiatePayment(string userId, decimal amount);
        Task<Result> VerifyPayment(string reference);
    }
}
