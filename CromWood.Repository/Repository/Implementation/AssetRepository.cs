using CromWood.Data.Context;
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
                        ScreenName = ScreenConstant.AssementManagement,
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
                        ScreenName = ScreenConstant.AssementManagement,
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
    }
}
