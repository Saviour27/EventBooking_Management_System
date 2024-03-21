using EventBooking_BE.Dtos;
using EventBooking_BE.Models;
using EventBooking_BE.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking_BE.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly UserManager<User> _userManager;

        public BookingController(IBookingService bookingService, UserManager<User> userManager)
        {
            _bookingService = bookingService;
            _userManager = userManager;
        }

        [HttpPost("book event/{eventId}")]
        public async Task<IActionResult> BookTicketWithWallet(string eventId)
        {
            var userId = User.FindFirst("sub")?.Value;
            var result = await _bookingService.BookTicketWithWallet(userId, eventId);
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<object>.Success());
        }

        [HttpPost("{id}/cancel booking")]
        public async Task<IActionResult> CancelBooking(string eventId)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _bookingService.CancelBooking(eventId, userId);
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<bool>.Success());
        }
    }
}
