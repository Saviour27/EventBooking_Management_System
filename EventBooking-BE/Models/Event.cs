namespace EventBooking_BE.Models
{
    public class Event : Entity
    {
        public string EventId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public decimal TicketPrice { get; set; }
        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
