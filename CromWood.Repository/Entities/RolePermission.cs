namespace CromWood.Data.Entities
{
    public class RolePermission: DBTable
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }
        public bool CanViewAll { get; set; }
    }
}
