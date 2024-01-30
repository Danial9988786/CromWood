using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using System.Linq.Dynamic.Core;

namespace CromWood.Data.Repository.Implementation
{
    public class TestRepository: ITestRepository
    {
        public TestRepository() { } 

        public async Task<IEnumerable<Test>> GetModifiedTests()
        {
            string condition = "x=>x.Name == \"Ananta Podel\"";
            var tests = new List<Test>() { new Test() { Id = Guid.NewGuid(), Name = "Ananta Poudel" } };
            var filtered = tests.AsQueryable().Where(condition).ToList();
            return filtered;
        }
    }
}
