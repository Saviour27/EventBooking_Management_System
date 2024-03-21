using EventBooking_BE.Dtos;
using EventBooking_BE.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking_BE.Controllers
{
    [ApiController]
    [Route("api/paystack")]
    public class PaystackController : ControllerBase
    {
        private readonly IPaystackService _paystackService;

        public PaystackController(IPaystackService paystackService)
        {
            _paystackService = paystackService;
        }

        [HttpPost("initiate-payment")]
        public async Task<IActionResult> InitiatePayment(decimal amount)
        {
            var userId = User.FindFirst("sub")?.Value;
            var result = await _paystackService.InitiatePayment(userId, amount);
            if (result.IsFailure)
                return BadRequest(ResponseDto<string>.Failure(result.Errors));

            return Ok(ResponseDto<string>.Success());
        }

        [HttpPost("verify-payment")]
        public async Task<IActionResult> VerifyPayment(string reference)
        {
            var result = await _paystackService.VerifyPayment(reference);
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<object>.Success());
        }
    }
}
