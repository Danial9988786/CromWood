﻿using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Data.Entities;

namespace CromWood.Business.Services.Interface
{
    public interface IPropertyService
    {
        public Task<AppResponse<IEnumerable<PropertyViewModel>>> GetPropertyForList();
        public Task<AppResponse<PropertyModel>> GetPropertyOverView(Guid propertyId);
        public Task<AppResponse<PropertyInsuranceModel>> GetPropertyInsuranceDetail(Guid propertyId);
        public Task<AppResponse<int>> AddProperty(PropertyModel property);
        public Task<AppResponse<int>> AddModifyInsurance(PropertyInsuranceModel property);
        public Task<AppResponse<IEnumerable<PropertyKeyModel>>> GetPropertyKeys(Guid propertyId);
        public Task<AppResponse<PropertyKeyModel>> GetPropertyKey(Guid id);
        public Task<AppResponse<int>> AddModifyKey(PropertyKeyModel key);
        public Task<AppResponse<int>> DeleteKey(Guid id);
    }
}