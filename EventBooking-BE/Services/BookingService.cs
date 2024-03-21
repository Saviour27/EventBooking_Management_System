using EventBooking_BE.Dtos;
using EventBooking_BE.Models;
using EventBooking_BE.ServiceAbstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventBooking_BE.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWalletTransactionService _walletService;

        private readonly UserManager<User> _userManager;

        public BookingService(IRepository repository, IUnitOfWork unitOfWork, UserManager<User> userManager, IWalletTransactionService walletService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _walletService = walletService;
        }

        


        public async Task<Result> BookTicketWithWallet(string userId, string eventId)
        {
            var walletBalanceResult = await _walletService.GetWalletBalance(userId);
            if (walletBalanceResult.IsFailure)
                return Result.Failure(walletBalanceResult.Errors);

            decimal walletBalance;
            if (!decimal.TryParse(walletBalanceResult.Value.ToString(), out walletBalance))
                return new Error[] { new Error("WalletBalanceParsingError", "Failed to parse wallet balance") };

            var eventDetails = await _repository.GetAll<Event>().FirstOrDefaultAsync(e => e.Id == eventId);
            if (eventDetails == null)
                return new Error[] { new Error("Event.NotFound", "Event not found") };

            if (walletBalance < eventDetails.TicketPrice)
                return new Error[] { new Error("Insufficient.Funds", "Insufficient funds in the wallet") };

            var deductionResult = await _walletService.DeductFromWallet(userId, eventDetails.TicketPrice);
            if (deductionResult.IsFailure)
                return Result.Failure(deductionResult.Errors);

            var booking = new Booking
            {
                UserId = userId,
                EventId = eventId,
            };

            await _repository.Add(booking);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> CancelBooking(string eventId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new Error[] { new Error("UserNotFound", "User not found") };

            var booking = user.BookedEvents.FirstOrDefault(eb => eb.EventId == eventId);
            if (booking == null)
                return new Error[] { new Error("BookingNotFound", "Booking not found") };

            user.BookedEvents.Remove(booking);
            booking.Event.AvailableTickets++;

            var ticketsToRefund = booking.Event.TotalTickets - booking.Event.AvailableTickets;
            if (ticketsToRefund > 0)
            {
                var refundAmount = ticketsToRefund * booking.Event.TicketPrice;

                var topUpResult = await _walletService.TopUpWallet(userId, refundAmount);
                if (topUpResult.IsFailure)
                    return Result.Failure(topUpResult.Errors);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}