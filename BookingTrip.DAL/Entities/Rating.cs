using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.DAL.Entities
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        [Required]
        [Range(1, 5)]
        public int Score { get; set; }

        [MaxLength(500)]
        public string Feedback { get; set; }

        public DateTime RatingDate { get; set; } = DateTime.UtcNow;
    }
}
