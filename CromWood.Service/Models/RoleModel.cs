using CromWood.Business.ViewModels;

namespace CromWood.Business.Models
{
    public class RoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<RolePermissionModel> RolePermission { get; set; }
    }

    public class RolePermissionModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public PermissionModel Permission { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }
        public bool CanViewAll { get; set; }
    }
}
