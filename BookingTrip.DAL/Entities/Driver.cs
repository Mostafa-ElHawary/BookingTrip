using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.DAL.Entities
{
    // Relationship: Driver has a one-to-one relationship with User.
    // Each Driver is a single user in the system.
    // Relationship: Driver has a one-to-many relationship with Trip.
    // Each driver can create multiple trips.
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string CarInfo { get; set; }

        public bool IsVerified { get; set; } = false;

        // Navigation properties
        public ICollection<Trip> Trips { get; set; }
    }
}
