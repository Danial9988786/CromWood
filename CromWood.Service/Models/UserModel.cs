using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This is a required field"), MaxLength(100, ErrorMessage = "Name should not be grater than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This is a required field"), MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "This is a required field"), MaxLength(500, ErrorMessage = "Password should not be grater than 500 characters"), PasswordPropertyText]
        public string Password { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
