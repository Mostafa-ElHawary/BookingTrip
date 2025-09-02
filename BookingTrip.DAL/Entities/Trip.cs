using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.DAL.Entities
{
    // Relationship: Trip has a many-to-one relationship with Driver.
    // Each trip is created by a single driver.
    // Relationship: Trip has a one-to-many relationship with Booking.
    // Each trip can contain multiple Bookings.
    public enum TripStatus
    {
        Scheduled,
        InProgress,
        Completed,
        Cancelled
    }

    public class Trip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        [Required]
        [MaxLength(100)]
        public string StartLocation { get; set; }

        [Required]
        [MaxLength(100)]
        public string EndLocation { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public int AvailableSeats { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SuggestedFare { get; set; }

        public TripStatus Status { get; set; } = TripStatus.Scheduled;

        // Navigation properties
        public ICollection<Booking> Bookings { get; set; }
    }
}
