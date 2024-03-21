using EventBooking_BE.Dtos;
using EventBooking_BE.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking_BE.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletTransactionService _walletService;

        public WalletController(IWalletTransactionService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("wallet balance")]
        public async Task<IActionResult> GetWalletBalance()
        {
            var userId = User.FindFirst("Jwt")?.Value;
            var result = await _walletService.GetWalletBalance(userId);
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<decimal>.Success());
        }

        [HttpPost("top up")]
        public async Task<IActionResult> TopUpWallet([FromBody] decimal amount)
        {
            var userId = User.FindFirst("sub")?.Value;
            var result = await _walletService.TopUpWallet(userId, amount);
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<object>.Success());
        }

        [HttpPost("deduct")]
        public async Task<IActionResult> DeductFromWallet([FromBody] decimal amount)
        {
            var userId = User.FindFirst("sub")?.Value;
            var result = await _walletService.DeductFromWallet(userId, amount);
            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<object>.Success());
        }
    }
}
