using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.DAL.Entities
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TripId { get; set; }
        public Trip Trip { get; set; }

        [Required]
        public int RiderId { get; set; }
        public Rider Rider { get; set; }

        [Required]
        [MaxLength(100)]
        public string PickupLocation { get; set; }

        [Required]
        [MaxLength(100)]
        public string DropoffLocation { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fare { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        // Navigation properties
        public Payment Payment { get; set; }
        public Rating Rating { get; set; }
    }
}
