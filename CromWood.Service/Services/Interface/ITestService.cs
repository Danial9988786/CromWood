using CromWood.Business.ViewModels;

namespace CromWood.Business.Services.Interface
{
    public interface ITestService
    {
        public Task<IEnumerable<TestViewModel>> GetTests();
    }
}
