using System.ComponentModel.DataAnnotations;

namespace BookingTrip.PL.Dtos
{
    public class BookingCreateDTO
    {
        [Required]
        public int TripId { get; set; }

        [Required]
        public int RiderId { get; set; }

        [Required]
        [StringLength(100)]
        public string PickupLocation { get; set; }

        [Required]
        [StringLength(100)]
        public string DropoffLocation { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Fare { get; set; }
    }

    public class BookingUpdateDTO
    {
        [StringLength(100)]
        public string PickupLocation { get; set; }

        [StringLength(100)]
        public string DropoffLocation { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Fare { get; set; }

        public BookingStatus? Status { get; set; }
    }

    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int RiderId { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public decimal Fare { get; set; }
        public BookingStatus Status { get; set; }
    }
}
