using CromWood.Data.Context;
using CromWood.Data.DTO;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CromWood.Data.Repository.Implementation
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        public AssetRepository(CromwoodContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Asset>> GetAssetsForList(Guid filterId)
        {

            if (filterId != Guid.Empty)
            {
                var condition = await GetFilterConiditon(filterId);
                var result = await _context.Assets.Where(condition).Include(x => x.AssetType).Include(x => x.FinancialPrgoram).Include(x => x.Properties).OrderByDescending(x => x.CreatedDate).ToListAsync();
                return result;
            }
            return await _context.Assets.Include(x => x.AssetType).Include(x => x.FinancialPrgoram).Include(x => x.Properties).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<Asset> GetAssetsOverView(Guid assetId)
        {
            return await _context.Assets.Include(x => x.AssetType).Include(x => x.FinancialPrgoram).FirstOrDefaultAsync(x => x.Id == assetId);
        }

        public async Task<int> AddAsset(Asset asset)
        {
            try
            {
                await _context.Assets.AddAsync(asset);

                var result = await _context.SaveChangesAsync();
                if (result == 1)
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Add,
                        ScreenName = ScreenConstant.AssetManagement,
                        ScreenDetailId = asset.Id,
                        Description = $"Added New Asset with Asset ID: {asset.AssetId}"
                    });
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> EditAsset(Asset asset)
        {
            try
            {
                var old = await _context.Assets.AsNoTracking().FirstOrDefaultAsync(x => x.Id == asset.Id);

                _context.Assets.Update(asset);
                var result = await _context.SaveChangesAsync();

                #region Adding change log statement
                if (result == 1)
                {
                    List<string> changedProperties = ObjectComparer.CompareObjects(old, asset);
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Edit,
                        ScreenName = ScreenConstant.AssetManagement,
                        ScreenDetailId = asset.Id,
                        Description = $"Changed Asset Folowing were changed: {string.Join(", ", changedProperties)}"
                    });
                }
                #endregion

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<AssetOverviewDto> GetAssetOverviewDTO(Guid assetId)
        {
            var assetOverviewDto = await _context.Assets
                .Where(a => a.Id == assetId)
                .Select(a => new
                {
                    Asset = a,
                    Properties = a.Properties.Select(p => new
                    {
                        Property = p,
                        Tenancies = p.Tenancies.Where(t => t.EndDate >= DateTime.Now).ToList(),
                        Tenants = p.Tenancies.SelectMany(t => t.Statements.SelectMany(ts => ts.Transactions)).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (assetOverviewDto == null || assetOverviewDto.Asset == null)
            {
                return null;
            }

            // Calculate total expected earning
            float expectedEarning = assetOverviewDto.Properties.Sum(p => p.Tenancies.Sum(t => t.RentAmount));

            // Calculate total actual earning
            float actualEarning = assetOverviewDto.Properties.Sum(p => p.Tenants.Sum(t => t.NetAmount));

            // Calculate total expenses
            float expenses = assetOverviewDto.Properties.Sum(p => p.Tenants.Sum(t => t.NetAmount));

            // Calculate total profit
            float totalProfit = actualEarning - expenses;

            // Map to DTO
            var assetOverview = new AssetOverviewDto
            {
                ExpectedEarning = expectedEarning,
                Earning = actualEarning,
                Expenses = expenses,
                TotalProfit = totalProfit,
                Properties = assetOverviewDto.Properties.Select(p => new AssetOverviewPropertyDetailDto
                {
                    Id=p.Property.Id,
                    PropertyID = p.Property.PropertyCode,
                    Status = p.Tenancies.Any() ? "Occupied" : "Vacant",
                    TenantName = p.Tenants.FirstOrDefault()?.PaidByTenantId.ToString(), 
                    TenancyStartDate = p.Tenancies.FirstOrDefault()?.StartDate,
                    TenancyEndDate = p.Tenancies.FirstOrDefault()?.EndDate,
                    TenancyDuration = Convert.ToInt32(((p.Tenancies.FirstOrDefault()?.EndDate ?? DateTime.Now) - (p.Tenancies.FirstOrDefault()?.StartDate ?? DateTime.Now)).TotalDays),
                    ExpectedEarning = p.Tenancies.Sum(t => t.RentAmount),
                    ActualEarning = p.Tenants.Sum(t => t.NetAmount)
                }).ToList(),
                TopPerformingProperties = new List<AssetOverviewPropertyDetailDto>()
            };

            return assetOverview;


        }

    }
}
