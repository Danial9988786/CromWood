using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IAssetRepository: IRepository<Asset>
    {
        public Task<IEnumerable<Asset>> GetAssetsForList();
        public Task<Asset> GetAssetsOverView(Guid assetId);
        public Task<int> AddAsset(Asset asset);
    }
}
