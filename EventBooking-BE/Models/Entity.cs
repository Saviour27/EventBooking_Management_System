namespace EventBooking_BE.Models
{
    public class Entity
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
    }
}
