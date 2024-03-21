using EventBooking_BE.Dtos;
using EventBooking_BE.Models;

namespace EventBooking_BE.ServiceAbstraction
{
    public interface IEventService
    {
        public Task<Result<IEnumerable<Event>>> GetEvents();
        Task<Result<IEnumerable<Event>>> GetAvailableEvents();
        Task<Result<IEnumerable<Event>>> GetBookedEvents();
        public Task<Result> CreateEvent(CreateEventDto eventDto);
        public Task<Result> UpdateEvent(string eventId, UpdateEventDto eventDto);
        public Task<Result> DeleteEvent(string eventId);
        public Task<Result<IEnumerable<EventBookingUserDto>>> GetEventRegistrationsList(string eventId);
    }
}
