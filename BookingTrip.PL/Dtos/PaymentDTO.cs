using System.ComponentModel.DataAnnotations;

namespace BookingTrip.PL.Dtos
{
    public class PaymentCreateDTO
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string Method { get; set; }
    }

    public class PaymentUpdateDTO
    {
        [Range(0, double.MaxValue)]
        public decimal? Amount { get; set; }

        [StringLength(50)]
        public string Method { get; set; }

        public PaymentStatus? Status { get; set; }
    }

    public class PaymentResponseDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
