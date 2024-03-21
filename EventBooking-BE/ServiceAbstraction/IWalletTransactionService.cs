using EventBooking_BE.Dtos;
using EventBooking_BE.Models;

namespace EventBooking_BE.ServiceAbstraction
{
    public interface IWalletTransactionService
    {
        Task<Result> GetWalletBalance(string userId);
        Task<Result> TopUpWallet(string userId, decimal amount);
        Task<Result> DeductFromWallet(string userId, decimal amount);
    }
}
