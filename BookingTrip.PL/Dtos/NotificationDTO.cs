using System.ComponentModel.DataAnnotations;

namespace BookingTrip.PL.Dtos
{
    public class NotificationCreateDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        [Required]
        public NotificationType Type { get; set; }
    }

    public class NotificationUpdateDTO
    {
        public bool? IsRead { get; set; }
    }

    public class NotificationResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
    }
}
