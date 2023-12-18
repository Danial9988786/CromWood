namespace CromWood.Business.Models
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NoOfUsers { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
