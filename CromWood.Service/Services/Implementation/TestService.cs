using CromWood.Business.Services.Interface;
using CromWood.Business.ViewModels;
using CromWood.Data.Repository.Interface;

namespace CromWood.Business.Services.Implementation
{
    public class TestService : ITestService
    {
        private readonly ITestRepository testRepository;
        public TestService(ITestRepository repository)
        {
            testRepository = repository;
        }
        public async Task<IEnumerable<TestModel>> GetTests()
        {
            var testViews = new List<TestModel>();
            var tests = await testRepository.GetModifiedTests();
            tests.ToList().ForEach((test) =>
            {
                testViews.Add(new TestModel()
                {
                    Name = test.Name,
                });
            });
            return testViews;
        }
    }
}
