using CromWood.Data.Entities;

namespace CromWood.Business.Models.ViewModel
{
    public class TenantViewModel:DBTable
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NIN { get; set; }
        public string Address { get; set; }
    }
}
