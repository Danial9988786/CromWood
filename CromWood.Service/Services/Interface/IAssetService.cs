using CromWood.Business.Helper;
using CromWood.Business.Models;

namespace CromWood.Business.Services.Interface
{
    public interface IAssetService
    {
        public Task<AppResponse<IEnumerable<AssetViewModel>>> GetAssetsForList();
        public Task<AppResponse<AssetModel>> GetAssetsOverView(Guid assetId);
        public Task<AppResponse<int>> AddAsset(AssetModel asset);
    }
}
