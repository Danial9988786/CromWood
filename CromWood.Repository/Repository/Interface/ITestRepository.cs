using CromWood.Data.DTO;
using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface ITestRepository
    {
        public Task<IEnumerable<Test>> GetModifiedTests();
        public Task<DashboardOverviewDTO> GetDashboardOverviewJSON();
    }
}
