using CromWood.Data.DTO;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Dynamic.Core;
using CromWood.Data.Context;

namespace CromWood.Data.Repository.Implementation
{
    public class TestRepository: ITestRepository
    {
        private readonly CromwoodContext _context;
        public TestRepository(CromwoodContext context) { 
            _context = context;
        } 

        public async Task<IEnumerable<Test>> GetModifiedTests()
        {
            string condition = "x=>x.Name == \"Ananta Podel\"";
            var tests = new List<Test>() { new Test() { Id = Guid.NewGuid(), Name = "Ananta Poudel" } };
            var filtered = tests.AsQueryable().Where(condition).ToList();
            return filtered;
        }

        public async Task<DashboardOverviewDTO> GetDashboardOverviewJSON()
        {

            var incomeExpenses = await _context.Statements
                .GroupBy(ts => new { ts.Date.Month })
                .Select(g => new IncomeExpenseDTO
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key.Month),
                    Income = g.Sum(ts => ts.NetAmount),
                    Expense = 0
                })
                .ToListAsync();

            for (int i = 1; i <= 12; i++)
            {
                var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(i);
                if (!incomeExpenses.Any(item => item.Month == monthName))
                {
                    incomeExpenses.Add(new IncomeExpenseDTO
                    {
                        Month = monthName,
                        Income = 0,
                        Expense = 0
                    });
                }
            }

            incomeExpenses.Sort((x, y) => DateTime.ParseExact(x.Month, "MMM", CultureInfo.InvariantCulture).Month.CompareTo(DateTime.ParseExact(y.Month, "MMM", CultureInfo.InvariantCulture).Month));

            // Finding out Properties vacancy.
            var properties = await _context.Properties.Include(x=>x.Tenancies).ToListAsync();
            var noTenancies = properties.Where(x => x.Tenancies == null || x.Tenancies.Count() == 0).Count();
            return new DashboardOverviewDTO
            {
                IncomeExpenses = incomeExpenses,
                Occupied = properties.Where(x => x.Tenancies.Any() && x.Tenancies.Max(t => t.EndDate) > DateTime.Now).Count(),
                Vaccant = properties.Where(x => x.Tenancies.Any() && x.Tenancies.Max(t => t.EndDate) < DateTime.Now).Count() + noTenancies,
                ExpiringSoon = properties.Where(x => x.Tenancies.Any() && x.Tenancies.Max(t => t.EndDate) > DateTime.Now && x.Tenancies.Max(t => t.EndDate).AddMonths(2) < DateTime.Now).Count(),
            };
        }
    }
}
