using EventBooking_BE.Dtos;
using EventBooking_BE.Models;
using EventBooking_BE.ServiceAbstraction;
using EventBooking_BE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking_BE.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var result = await _eventService.GetEvents();
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<IEnumerable<Event>>.Success(result.Data));
        }

        [HttpGet("available event")]
        public async Task<IActionResult> GetAvailableEvents()
        {
            var result = await _eventService.GetAvailableEvents();
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<IEnumerable<Event>>.Success(result.Data));
        }

        [HttpGet("booked-events")]
        public async Task<IActionResult> GetBookedEvents()
        {
            var result = await _eventService.GetBookedEvents();
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<IEnumerable<Event>>.Success(result.Data));
        }

        [HttpPost("create event")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
        {
            var result = await _eventService.CreateEvent(eventDto);
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<int>.Success());
        }

        [HttpPut("update event {id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent(string id, [FromBody] UpdateEventDto eventDto)
        {
            var result = await _eventService.UpdateEvent(id, eventDto);
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<bool>.Success());
        }

        [HttpDelete("delete event {id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var result = await _eventService.DeleteEvent(id);
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<bool>.Success());
        }

        [HttpGet("{id}/registrations")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEventRegistrations(string id)
        {
            var result = await _eventService.GetEventRegistrationsList(id);
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<IEnumerable<User>>.Success());
        }

    }
}
