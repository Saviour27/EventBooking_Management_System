using EventBooking_BE.Dtos;
using EventBooking_BE.ServiceAbstraction;
using Newtonsoft.Json.Linq;


namespace EventBooking_BE.Services
{
    public class PaystackService : IPaystackService
    {
        private readonly HttpClient _httpClient;
        private readonly string _paystackApiKey;

        public PaystackService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _paystackApiKey = configuration["Paystack:ApiKey"];
        }

        public async Task<Result<string>> InitiatePayment(string userId, decimal amount)
        {
            var paystackPayload = new
            {
                email = GetUserEmail(userId),
                amount = amount * 100,
                reference = GeneratePaymentReference(),
                callback_url = "https://yourdomain.com/paystack/callback"
            };

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_paystackApiKey}");

            var paystackResponse = await _httpClient.PostAsJsonAsync("/initialize", paystackPayload);

            if (!paystackResponse.IsSuccessStatusCode)
            {
                return new Error[] { new("Message.Error", "Failed to initiate payment with Paystack") };
            }

            var responseContent = await paystackResponse.Content.ReadAsStringAsync();

            var responseObject = JObject.Parse(responseContent);
            var authorizationUrl = responseObject["authorization_url"].ToString();

            return Result<string>.Success(authorizationUrl);
        }

        public async Task<Result> VerifyPayment(string reference)
        {
            var paystackResponse = await _httpClient.GetAsync($"/verify/{reference}");

            if (!paystackResponse.IsSuccessStatusCode)
            {
                return new Error[] { new("Message.Error", "Failed to verify payment with Paystack") };
            }

            return Result.Success();
        }

        private string GetUserEmail(string userId)
        {
            return "user@example.com";
        }

        private string GeneratePaymentReference()
        {
            return "PAYSTACK_REF_123456";
        }
    }
}

