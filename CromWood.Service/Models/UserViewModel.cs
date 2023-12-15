namespace CromWood.Business.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastActive { get; set; }
    }
}
