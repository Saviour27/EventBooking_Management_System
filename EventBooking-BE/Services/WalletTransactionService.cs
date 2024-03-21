using EventBooking_BE.Dtos;
using EventBooking_BE.Models;
using EventBooking_BE.ServiceAbstraction;
using Microsoft.EntityFrameworkCore;

namespace EventBooking_BE.Services
{
    public class WalletTransactionService : IWalletTransactionService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public WalletTransactionService(IRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> GetWalletBalance(string userId)
        {
            var wallet = await _repository.GetAll<WalletTransaction>()
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wallet == null)
                return new Error[] { new("Wallet.NotFound", "Wallet not found") };

            return Result.Success();
        }

        public async Task<Result> TopUpWallet(string userId, decimal amount)
        {
            var wallet = await _repository.GetAll<WalletTransaction>()
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wallet == null)
            {
                wallet = new WalletTransaction { UserId = userId, Balance = amount };
                await _repository.Add(wallet);
            }
            else
            {
                wallet.Balance += amount;
                _repository.Update(wallet);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeductFromWallet(string userId, decimal amount)
        {
            var wallet = await _repository.GetAll<WalletTransaction>()
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wallet == null)
                return new Error[] { new("Wallet.NotFound", "Wallet not found") };

            if (wallet.Balance < amount)
                return new Error[] { new("Wallet.Error", "Insufficient fund") }; ;

            wallet.Balance -= amount;
            _repository.Update(wallet);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}