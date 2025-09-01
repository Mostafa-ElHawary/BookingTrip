using System.ComponentModel.DataAnnotations;

namespace BookingTrip.PL.Dtos
{
    public class DriverCreateDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string CarInfo { get; set; }
    }

    public class DriverUpdateDTO
    {
        [StringLength(50)]
        public string LicenseNumber { get; set; }

        [StringLength(200)]
        public string CarInfo { get; set; }

        public bool? IsVerified { get; set; }
    }

    public class DriverResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LicenseNumber { get; set; }
        public string CarInfo { get; set; }
        public bool IsVerified { get; set; }
    }
    public class DriverProfileDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LicenseNumber { get; set; }
        public string CarInfo { get; set; }
        public bool IsVerified { get; set; }
    }
}
