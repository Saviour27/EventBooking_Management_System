using EventBooking_BE.Models;
using Microsoft.AspNetCore.Identity;

namespace EventBooking_BE.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public decimal WalletBalance { get; set; }

        // Navigation property to represent the events booked by the user
        public ICollection<Booking> BookedEvents { get; set; }
    }
}
