using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBooking_BE.Models
{
    public class UserEvent
    {
        [Key]
        public int UserId { get; set; }

        public int EventId { get; set; }

        public User User { get; set; }

        public Event Event { get; set; }
    }
}
