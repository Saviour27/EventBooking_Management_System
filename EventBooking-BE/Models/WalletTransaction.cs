namespace EventBooking_BE.Models
{
    public class WalletTransaction : Entity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime TransactionTime { get; set; }
        public string Description { get; set; }
    }
}
