using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.DAL.Entities
{
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
