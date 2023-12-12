using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;

namespace CromWood.Data.Repository.Implementation
{
    public class TestRepository: ITestRepository
    {
        public TestRepository() { } 

        public async Task<IEnumerable<Test>> GetModifiedTests()
        {
            var tests = new List<Test>() { new Test() { Id = Guid.NewGuid(), Name = "Ananta Poudel" } };
            return tests;
        }
    }
}
