namespace EventBooking_BE.Models
{
    public class Booking : Entity
    {
        public string BookingId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string EventId { get; set; }
        public Event Event { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
