using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.DAL.Entities
{
    // Relationship: Rider has a one-to-one relationship with User.
    // Each Rider is a single user in the system.
    // Relationship: Rider has a one-to-many relationship with Booking.
    // Each rider can make multiple Bookings.
    public class Rider
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation properties
        public ICollection<Booking> Bookings { get; set; }
    }
}
