using EventBooking_BE.Dtos;
using EventBooking_BE.Models;
using EventBooking_BE.ServiceAbstraction;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Event>>> GetEvents()
        {
            var events = await _repository.GetAll<Event>().ToListAsync();
            return events;
        }

        public async Task<Result<IEnumerable<Event>>> GetAvailableEvents()
        {
            var events = await _repository.GetAll<Event>()
                .Where(e => e.AvailableTickets > 0)
                .ToListAsync();

            return events;
        }

        public async Task<Result<IEnumerable<Event>>> GetBookedEvents()
        {
            var events = await _repository.GetAll<Event>()
                .Where(e => e.AvailableTickets == 0)
                .ToListAsync();

            return events;
        }

        public async Task<Result> CreateEvent(CreateEventDto eventDto)
        {
            var newEvent = new Event
            {
                Name = eventDto.Name,
                StartTime = eventDto.StartTime,
                EndTime = eventDto.EndTime,
                Location = eventDto.Location,
                TicketPrice = eventDto.TicketPrice,
                TotalTickets = eventDto.TotalTickets
            };

            await _repository.Add(newEvent);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> UpdateEvent(string eventId, UpdateEventDto eventDto)
        {
            var existingEvent = await _repository.FindById<Event>(eventId);

            if (existingEvent == null)
            {
                return new Error[] { new("existingEvent.Error", "No Existing Event") };
            }

            existingEvent.Name = eventDto.Name ?? existingEvent.Name;
            existingEvent.StartTime = eventDto.StartTime ?? existingEvent.StartTime;
            existingEvent.EndTime = eventDto.EndTime ?? existingEvent.EndTime;
            existingEvent.Location = eventDto.Location ?? existingEvent.Location;
            existingEvent.TicketPrice = eventDto.TicketPrice ?? existingEvent.TicketPrice;
            existingEvent.TotalTickets = eventDto.TotalTickets ?? existingEvent.TotalTickets;

            _repository.Update(existingEvent);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteEvent(string eventId)
        {
            var existingEvent = _repository.GetAll<Event>().FirstOrDefault(e => e.Id == eventId);

            if (existingEvent == null)
            {
                return new Error[] { new("existingEvent.Error", "No Existing Event") };
            }

            _repository.Remove(existingEvent);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result<IEnumerable<EventBookingUserDto>>> GetEventRegistrationsList(string eventId)
        {
            var registrations = await _repository.GetAll<Event>()
                .Include(e => e.Users) // Include the Users navigation property
                .Where(e => e.EventId == eventId)
                .SelectMany(e => e.Users)
                .Select(u => new EventBookingUserDto
                {
                    UserId = u.Id,
                    UserName = u.UserName
                })
                .ToListAsync();

            return registrations;
        }

    }
}