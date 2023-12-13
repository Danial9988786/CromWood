using System.ComponentModel.DataAnnotations;

namespace CromWood.Data.Entities
{
    public class Role: DBTable
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        public ICollection<RolePermission> Permissions { get; set; }
    }
}
