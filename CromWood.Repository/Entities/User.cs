using System.ComponentModel.DataAnnotations;

namespace CromWood.Data.Entities
{
    public class User: DBTable
    {
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(25)]
        public string Phone { get; set; }

        [Required, MaxLength(500)]
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        // When User is Blocked this flag will be false.
        public bool IsActive { get; set; }
        public DateTime? LastActive { get; set; }
        public DateTime? OTPExpirationDate { get; set; }
        public string OTP { get; set; }
    }
}
