﻿using CromWood.Data.DTO;
using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IAssetRepository: IRepository<Asset>
    {
        public Task<IEnumerable<Asset>> GetAssetsForList(Guid filterId);
        public Task<Asset> GetAssetsOverView(Guid assetId);
        public Task<int> AddAsset(Asset asset);
        public Task<int> EditAsset(Asset asset);
        public Task<AssetOverviewDto> GetAssetOverviewDTO(Guid assetId);
    }
}
