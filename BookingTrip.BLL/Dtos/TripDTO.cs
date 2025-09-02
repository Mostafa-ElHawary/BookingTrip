using System.ComponentModel.DataAnnotations;
using BookingTrip.DAL.Entities;

namespace BookingTrip.PL.Dtos
{
    public class TripCreateDTO
    {
        [Required]
        public int DriverId { get; set; }

        [Required]
        [StringLength(100)]
        public string StartLocation { get; set; }

        [Required]
        [StringLength(100)]
        public string EndLocation { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AvailableSeats { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SuggestedFare { get; set; }
    }

    public class TripUpdateDTO
    {
        [StringLength(100)]
        public string StartLocation { get; set; }

        [StringLength(100)]
        public string EndLocation { get; set; }

        public DateTime? StartTime { get; set; }

        [Range(0, int.MaxValue)]
        public int? AvailableSeats { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? SuggestedFare { get; set; }

        public TripStatus? Status { get; set; }
    }

    public class TripResponseDTO
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartTime { get; set; }
        public int AvailableSeats { get; set; }
        public decimal SuggestedFare { get; set; }
        public TripStatus Status { get; set; }
    }

    public class TripPublishDTO
    {
        [Required]
        public int DriverId { get; set; }

        [Required]
        [StringLength(100)]
        public string StartLocation { get; set; }

        [Required]
        [StringLength(100)]
        public string EndLocation { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AvailableSeats { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SuggestedFare { get; set; }
    }

    public class TripSearchDTO
    {
        [Required]
        [StringLength(100)]
        public string StartLocation { get; set; }

        [Required]
        [StringLength(100)]
        public string EndLocation { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
    }

    public class TripDetailsDTO
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartTime { get; set; }
        public int AvailableSeats { get; set; }
        public decimal SuggestedFare { get; set; }
        public TripStatus Status { get; set; }
    }
}
