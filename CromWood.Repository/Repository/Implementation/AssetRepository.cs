using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
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

            if(filterId != Guid.Empty)
            {
                var condition = await GetFilterConiditon(filterId);
                var result = await _context.Assets.Where(condition).Include(x => x.AssetType).Include(x => x.FinancialPrgoram).Include(x => x.Properties).OrderByDescending(x => x.CreatedDate).ToListAsync();
                return result;
            }
            return await _context.Assets.Include(x=>x.AssetType).Include(x=>x.FinancialPrgoram).Include(x=>x.Properties).OrderByDescending(x=>x.CreatedDate).ToListAsync();
        }

        public async Task<Asset> GetAssetsOverView(Guid assetId)
        {
            return await _context.Assets.Include(x=>x.AssetType).Include(x=>x.FinancialPrgoram).FirstOrDefaultAsync(x => x.Id == assetId);
        }

        public async Task<int> AddAsset(Asset asset)
        {
            try
            {
                await _context.Assets.AddAsync(asset);
                await _context.SaveChangesAsync();
                return 1;
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
                _context.Assets.Update(asset);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}
