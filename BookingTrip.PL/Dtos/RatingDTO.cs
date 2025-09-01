using System.ComponentModel.DataAnnotations;

namespace BookingTrip.PL.Dtos
{
    public class RatingCreateDTO
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Score { get; set; }

        [StringLength(500)]
        public string Feedback { get; set; }
    }

    public class RatingUpdateDTO
    {
        [Range(1, 5)]
        public int? Score { get; set; }

        [StringLength(500)]
        public string Feedback { get; set; }
    }

    public class RatingResponseDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int Score { get; set; }
        public string Feedback { get; set; }
        public DateTime RatingDate { get; set; }
    }
}
