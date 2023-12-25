using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;

namespace CromWood.Business.Services.Implementation
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        public AssetService(IAssetRepository assetRepository, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
        }

        public async Task<AppResponse<IEnumerable<AssetViewModel>>> GetAssetsForList()
        {
            try
            {
                var result = await _assetRepository.GetAssetsForList();
                var mappedResult = _mapper.Map<IEnumerable<AssetViewModel>>(result);
                return ResponseCreater<IEnumerable<AssetViewModel>>.CreateSuccessResponse(mappedResult, "Assets loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<AssetViewModel>>.CreateErrorResponse(null, ex.ToString());
            }

        }

        public async Task<AppResponse<AssetModel>> GetAssetsOverView(Guid assetId)
        {
            try
            {
                var result = await _assetRepository.GetAssetsOverView(assetId);
                var mappedResult = _mapper.Map<AssetModel>(result);
                return ResponseCreater<AssetModel>.CreateSuccessResponse(mappedResult, "Asset overview loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<AssetModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddAsset(AssetModel asset)
        {
            try
            {
                var mappedAsset = _mapper.Map<Asset>(asset);
                var result = await _assetRepository.AddAsset(mappedAsset);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Asset added successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> EditAsset(AssetModel asset)
        {
            try
            {
                var mappedAsset = _mapper.Map<Asset>(asset);
                var result = await _assetRepository.EditAsset(mappedAsset);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Asset edited successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }
    }
}
