using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        public AssetRepository(CromwoodContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Asset>> GetAssetsForList()
        {
            return await _context.Assets.Include(x=>x.AssetType).ToListAsync();
        }

        public async Task<Asset> GetAssetsOverView(Guid assetId)
        {
            return await _context.Assets.Include(x=>x.AssetType).FirstOrDefaultAsync(x => x.Id == assetId);
        }

        public async Task<int> AddAsset(Asset asset)
        {
            try
            {
                asset.Id = Guid.NewGuid();
                await _context.Assets.AddAsync(asset);
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
