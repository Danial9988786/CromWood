using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.ViewModels;

namespace CromWood.Business.Services.Interface
{
    public interface ITestService
    {
        public Task<IEnumerable<TestModel>> GetTests();
        public Task<AppResponse<DashboardModel>> GetDashboardOverview();
        public Task<AppResponse<DashboardModel>> GetDashboardOverviewJSON();
    }
}
