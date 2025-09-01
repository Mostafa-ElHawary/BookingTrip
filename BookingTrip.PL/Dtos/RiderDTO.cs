using System.ComponentModel.DataAnnotations;

namespace BookingTrip.PL.Dtos
{
    public class RiderCreateDTO
    {
        [Required]
        public int UserId { get; set; }
    }

    public class RiderResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
