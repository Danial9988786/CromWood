using CromWood.Data.Context;
using CromWood.Data.DTO;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Dynamic.Core;

namespace CromWood.Data.Repository.Implementation
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(CromwoodContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Property>> GetPropertyForList(Guid filterId)
        {
            if (filterId != Guid.Empty)
            {
                var condition = await GetFilterConiditon(filterId);
                var result = await _context.Properties.Where(condition).Include(x => x.Asset).Include(x => x.PropertyType).Include(x => x.Tenancies).ThenInclude(x => x.TenancyTenants).ThenInclude(x => x.Tenant).OrderByDescending(x => x.CreatedDate).ToListAsync();
                return result;
            }
            return await _context.Properties.Include(x => x.Asset).Include(x => x.PropertyType).Include(x => x.Tenancies).ThenInclude(x => x.TenancyTenants).ThenInclude(x => x.Tenant).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<Property> GetPropertyOverView(Guid PropertyId)
        {
            return await _context.Properties.Include(x => x.Asset).Include(x => x.PropertyAmenities).ThenInclude(x => x.Amenity).Include(x => x.PropertyType).Include(x => x.Tenancies).FirstOrDefaultAsync(x => x.Id == PropertyId);
        }

        public async Task<PropertyInsurance> GetPropertyInsurance(Guid PropertyId)
        {
            return await _context.PropertyInsurances.FirstOrDefaultAsync(x => x.PropertyId == PropertyId);
        }

        public async Task<Guid> AddModifyProperty(Property Property)
        {
            try
            {
                var changelog = new ChangeLog();
                if (Property.Id == Guid.Empty)
                {
                    Property.Id = Guid.NewGuid();
                    await _context.Properties.AddAsync(Property);

                    changelog = new ChangeLog()
                    {
                        Type = ChangeType.Add,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = Property.Id,
                        Description = $"Added New Property with Property ID: {Property.PropertyCode}"
                    };
                }
                else
                {
                    var old = await _context.Properties.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Property.Id);
                    _context.Update(Property);

                    List<string> changedProperties = ObjectComparer.CompareObjects(old, Property);
                    changelog = new ChangeLog()
                    {
                        Type = ChangeType.Edit,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = Property.Id,
                        Description = $"Changed Property Folowing were changed: {string.Join(", ", changedProperties)}"
                    };
                }
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                    await AddChangeLog(changelog);
                return Property.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddInsurance(PropertyInsurance insurance)
        {
            try
            {
                insurance.Id = Guid.NewGuid();
                await _context.PropertyInsurances.AddAsync(insurance);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Add,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = insurance.PropertyId,
                        Description = $"Added New Insurance to Property"
                    });
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ModifyInsurance(PropertyInsurance insurance)
        {
            try
            {
                _context.PropertyInsurances.Update(insurance);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Edit,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = insurance.PropertyId,
                        Description = $"Modified Insurance of the Property"
                    });
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PropertyKey>> GetPropertyKeys(Guid propertyId)
        {
            return await _context.PropertyKeys.Where(x => x.PropertyId == propertyId).Include(x => x.PropertyKeyType).ToListAsync();
        }

        public async Task<PropertyKey> GetPropertyKey(Guid id)
        {
            return await _context.PropertyKeys.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddKey(PropertyKey key)
        {
            try
            {
                key.Id = Guid.NewGuid();
                await _context.PropertyKeys.AddAsync(key);
                await _context.SaveChangesAsync();
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Add,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = key.PropertyId,
                        Description = $"Added New Key to Property: {key.Name}"
                    });
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ModifyKey(PropertyKey key)
        {
            try
            {
                _context.PropertyKeys.Update(key);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Edit,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = key.PropertyId,
                        Description = $"Updated Key of the Property: {key.Name}"
                    });
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteKey(Guid id)
        {
            try
            {
                var key = await _context.PropertyKeys.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.PropertyKeys.Remove(key);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Delete,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = key.PropertyId,
                        Description = $"Deleted Key of the Property: {key.Name}"
                    });
                }
                return key.ImageUrl;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Tenancy>> GetPropertyTenancy(Guid id)
        {
            return await _context.Tenancies.Where(x => x.PropertyId == id).Include(x => x.TenancyTenants).ThenInclude(y => y.Tenant).ToListAsync();
        }

        public async Task<IEnumerable<Complaint>> GetRecentComplaints(Guid propertyId)
        {
            return await _context.Complaints.Where(x => x.PropertyId == propertyId).Include(x => x.ComplaintCategory).Include(x => x.ComplaintNature).Include(x => x.Tenant).ToListAsync();
        }

        public async Task<IEnumerable<TenancyStatement>> GetRecentTransactions(Guid propertyId)
        {
            return await _context.Statements.Include(x => x.StatementType).Include(x=>x.Tenancy).Where(x => x.Tenancy.PropertyId == propertyId).ToListAsync();
        }

        public async Task<PropertyOverviewDTO> GetPropertyOverview(Guid propertyId)
        {
            var today = DateTime.Today;

            // Fetch occupied and vacant days
            var occupiedDays = await CalculateDays(propertyId, today, true);
            var vacantDays = await CalculateDays(propertyId, today, false);

            // Fetch income and expenses data
            var incomeExpenses = await _context.Statements
                .Where(ts => ts.Tenancy.PropertyId == propertyId)
                .GroupBy(ts => new { ts.Date.Month })
                .Select(g => new IncomeExpenseDTO
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key.Month),
                    Income = g.Sum(ts => ts.NetAmount),
                    Expense = 0 // Initialize expenses to 0 as we're not fetching expense data
                })
                .ToListAsync();

            // Ensure all 12 months are included in the incomeExpenses list
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

            // Sort incomeExpenses by month
            incomeExpenses.Sort((x, y) => DateTime.ParseExact(x.Month, "MMM", CultureInfo.InvariantCulture).Month.CompareTo(DateTime.ParseExact(y.Month, "MMM", CultureInfo.InvariantCulture).Month));

            // Fetch occupied days data
            var occupiedVacants = new List<OccupiedVacantDTO>();

            // Get the current year
            var currentYear = DateTime.Today.Year;

            // Iterate over the months from January to December
            for (int month = 1; month <= 12; month++)
            {
                // Calculate the start and end dates for the current month
                var startDate = new DateTime(currentYear, month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                // Fetch the tenancies for the current month
                var tenancies = await _context.Tenancies
                    .Where(t => t.PropertyId == propertyId &&
                                t.StartDate <= endDate && t.EndDate >= startDate)
                    .ToListAsync();

                // Calculate the total occupied days for the current month
                var occupiedDaysEach = tenancies.Sum(t =>
                {
                    var start = t.StartDate > startDate ? t.StartDate : startDate;
                    var end = t.EndDate < endDate ? t.EndDate : endDate;
                    return (int)(end - start).TotalDays;
                });

                // Create a new OccupiedVacantDTO object for the current month
                var occupiedVacant = new OccupiedVacantDTO
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month),
                    OccupiedDays = occupiedDaysEach
                };

                // Add the OccupiedVacantDTO object to the list
                occupiedVacants.Add(occupiedVacant);
            }


            // Sort occupiedVacants by month
            occupiedVacants.Sort((x, y) => DateTime.ParseExact(x.Month, "MMM", CultureInfo.InvariantCulture).Month.CompareTo(DateTime.ParseExact(y.Month, "MMM", CultureInfo.InvariantCulture).Month));

            // Leave RecentTransactions and RecentComplaints empty as requested
            return new PropertyOverviewDTO
            {
                OccupiedDay = occupiedDays,
                VacantDay = vacantDays,
                IncomeExpenses = incomeExpenses,
                OccupiedVacants = occupiedVacants
            };
        }

        public async Task<int> CalculateDays(Guid propertyId, DateTime today, bool isOccupied)
        {
            // Get the end date of the current year
            var endOfYear = new DateTime(today.Year, 12, 31);

            // Fetch the tenancies based on the provided criteria
            var tenancies = await _context.Tenancies
                .Where(t => t.PropertyId == propertyId &&
                            (isOccupied ? (t.StartDate <= today && t.EndDate >= today) :
                                         (t.StartDate > today || t.EndDate < today)))
                .Select(t => new { t.StartDate, t.EndDate })
                .ToListAsync();

            // Calculate the total occupied days
            var days = tenancies.Sum(t =>
            {
                var startDate = t.StartDate < today ? today : t.StartDate;
                var endDate = t.EndDate < endOfYear ? t.EndDate : endOfYear;
                return (int)(endDate - startDate).TotalDays;
            });

            return days;
        }



    }
}
