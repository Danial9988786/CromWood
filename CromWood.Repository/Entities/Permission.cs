using System.ComponentModel.DataAnnotations;

namespace CromWood.Data.Entities
{
    public class Permission
    {
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string PermissionKey { get; set; }

        [Required, MaxLength(100)]
        public string PermissionDisplayName { get; set; }
    }
}
