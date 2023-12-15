using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(500)]
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
