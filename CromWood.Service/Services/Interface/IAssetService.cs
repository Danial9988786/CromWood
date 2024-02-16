using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;

namespace CromWood.Business.Services.Interface
{
    public interface IAssetService
    {
        public Task<AppResponse<IEnumerable<AssetViewModel>>> GetAssetsForList(Guid filterId );
        public Task<AppResponse<IEnumerable<AssetModel>>> GetAssetsForExport(Guid filterId );
        public Task<AppResponse<AssetModel>> GetAssetsOverView(Guid assetId);
        public Task<AppResponse<int>> AddAsset(AssetModel asset);
        public Task<AppResponse<int>> EditAsset(AssetModel asset);
        public Task<AppResponse<AssetOverviewModel>> GetAssetsOverViewPageDetail(Guid assetId);
    }
}
