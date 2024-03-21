namespace EventBooking_BE.Dtos
{
    public class AvailableEventListDto
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public decimal TicketPrice { get; set; }
    }
}
