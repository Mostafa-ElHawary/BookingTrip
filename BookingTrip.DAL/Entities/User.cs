using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.DAL.Entities
{
    // Relationship: User is the central point from which relationships branch out.
    // Relationship: User has a one-to-one relationship with Driver.
    // Relationship: User has a one-to-one relationship with Rider.
    // Relationship: User has a one-to-many relationship with Notification.
    // Relationship: User has a one-to-one relationship with Wallet.
    public enum UserRole
    {
        Admin,
        Driver,
        Rider
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public Driver Driver { get; set; }
        public Rider Rider { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public Wallet Wallet { get; set; }
    }
}
