using CromWood.Business.Constants;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Data;
using CromWood.Data.Context;
using CromWood.Data.Entities.Default;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CromWood.Controllers
{
    public class FilterController : Controller
    {
        private readonly CromwoodContext _context;

        public FilterController(CromwoodContext cromwood)
        {
            _context = cromwood;
        }

        public async Task<IActionResult> Index(string keyName)
        {
            try
            {
                ViewBag.ScreenName = keyName;

                var props = GetFilterForPage(keyName);
                var columnsForFilter = props.Where(x => x.CustomAttributes.Count() > 0 && x.CustomAttributes.Any(y => y.NamedArguments.Count > 0)).ToList();

                var filterColumns = new List<FilterColumn>();
                foreach (var col in columnsForFilter)
                {
                    var colType = Nullable.GetUnderlyingType(col.PropertyType) != null ?
                              Type.GetTypeCode(Nullable.GetUnderlyingType(col.PropertyType)).ToString() :
                              Type.GetTypeCode(col.PropertyType).ToString();
                    filterColumns.Add(new FilterColumn()
                    {
                        Type = colType == "Object" ? "Guid" : colType,
                        Name = col.Name,
                        // In case of Guid, we have to see what to fetch in select dropdown
                        FetchType = colType == "Object" ? col.Name.Split("Id")[0] : null,
                        DisplayName = col.CustomAttributes.First(x => x.NamedArguments.Count > 0).NamedArguments.FirstOrDefault().TypedValue.Value?.ToString()
                    });
                }

                ViewBag.FilterColumns = filterColumns;

                var result = await _context.Filters.Where(x => x.PageName == keyName).ToListAsync();
                return PartialView(result);
            }
            catch (Exception ex)
            {
                return PartialView();
            }
        }

        public async Task<IActionResult> GetAssetType([FromServices] ILookupService<AssetType> service)
        {
            var result = await service.GetAllItems();
            return Ok(result);
        }

        public async Task<IActionResult> GetFinancialPrgoram([FromServices] ILookupService<FinancialPrgoram> service)
        {
            var result = await service.GetAllItems();
            return Ok(result);
        }

        public async Task<IActionResult> GetPropertyType([FromServices] ILookupService<PropertyType> service)
        {
            var result = await service.GetAllItems();
            return Ok(result);
        }
        public async Task<IActionResult> GetLicenseCertificateType([FromServices] ILookupService<LicenseCertificateType> service)
        {
            var result = await service.GetAllItems();
            return Ok(result);
        }

        public async Task<IActionResult> GetCountry([FromServices] ILookupService<Country> service)
        {
            var result = await service.GetAllItems();
            return Ok(result);
        }

        public async Task<IActionResult> GetSalutation([FromServices] ILookupService<Salution> service)
        {
            var result = await service.GetAllItems();
            return Ok(result);
        }

        public async Task<IActionResult> AddFilter(Filter filter)
        {
            var result = await _context.Filters.AddAsync(filter);
            await _context.SaveChangesAsync();
            return Ok(result);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var filter = await _context.Filters.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (filter != null)
                _context.Filters.Remove(filter);
            await _context.SaveChangesAsync();
            return Ok(1);
        }

        public PropertyInfo[] GetFilterForPage(string keyName)
        {
            switch (keyName)
            {
                case PermissionKeyConstant.AssetManagement:
                    return typeof(AssetModel).GetProperties();
                case PermissionKeyConstant.PropertyManagement:
                    return typeof(PropertyModel).GetProperties();
                case PermissionKeyConstant.LicenseManagement:
                    return typeof(LicenseCertificateModel).GetProperties();
                case PermissionKeyConstant.AssementManagement:
                    return typeof(PropertyAssesmentModel).GetProperties();
                case PermissionKeyConstant.TenantManagement:
                    return typeof(TenantModel).GetProperties();
                default:
                    return new PropertyInfo[] { };
            }
        }
    }
}
