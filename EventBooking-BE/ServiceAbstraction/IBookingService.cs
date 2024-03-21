using EventBooking_BE.Dtos;
using EventBooking_BE.Models;

namespace EventBooking_BE.ServiceAbstraction
{
    public interface IBookingService
    {
        
        Task<Result> BookTicketWithWallet(string userId, string eventId);
        Task<Result> CancelBooking(string eventId, string userId);
    }
}
