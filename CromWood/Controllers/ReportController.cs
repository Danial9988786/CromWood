using CromWood.Business.Constants;
using CromWood.Business.Models;
using CromWood.Data.Context;
using CromWood.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CromWood.Controllers
{
    public class ReportController : Controller
    {
        private readonly CromwoodContext _context;

        public ReportController(CromwoodContext cromwood)
        {
            _context = cromwood;
        }

        public async Task<IActionResult> Index()
        {
           var reports = await _context.CustomReports.ToListAsync();
           return View(reports);
        }

        public IActionResult CreateReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport(CustomReport report)
        {
            var cus = report;
            await _context.CustomReports.AddAsync(cus);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public IActionResult GetReportProperties(string keyName)
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
                return Ok(filterColumns);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
