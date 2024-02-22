using CromWood.Business.Constants;
using CromWood.Business.Models;
using CromWood.Data.Context;
using CromWood.Data.Entities;
using DocumentFormat.OpenXml.Bibliography;
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
        //Create and Edit
        [HttpPost]
        public async Task<IActionResult> CreateReport(CustomReport report)
        {
            try
            {
                // Fetching Existing Report Data
                var ExistingReport = _context.CustomReports.Where(x => x.Id.ToString().ToLower().Equals(report.Id.ToString().ToLower())).FirstOrDefault();
                if (ExistingReport != null)
                {
                    ExistingReport.ReportGroup = report.ReportGroup;
                    ExistingReport.Favourite = report.Favourite;
                    ExistingReport.Name = report.Name;
                    ExistingReport.Description = report.Description;
                    if (report.CustomReportAttributes != null && report.CustomReportAttributes.Count > 0)
                    {
                        foreach (var reportAttribute in report.CustomReportAttributes)
                        {
                            // Fetching data through same DataName and Same report id for update that record
                            var existingcustomreportattributes = _context.CustomReportAttributes.Where(x => x.DataName.Equals(reportAttribute.DataName) && x.CustomReportId.ToString().ToLower().Equals(report.Id.ToString().ToLower())).FirstOrDefault();
                            if (existingcustomreportattributes != null)
                            {
                                existingcustomreportattributes.Alignment = reportAttribute.Alignment;
                                existingcustomreportattributes.Width = reportAttribute.Width;
                                existingcustomreportattributes.Append = reportAttribute.Append;
                                existingcustomreportattributes.Prepend = reportAttribute.Prepend;
                                _context.CustomReportAttributes.Update(existingcustomreportattributes);
                                _context.SaveChanges();
                            }
                            else
                            {
                                //If not exist create new report attricute against that reportid
                                var CustomReportAttributeobject = new CustomReportAttribute()
                                {
                                    CustomReportId = report.Id,
                                    HeaderName = reportAttribute.HeaderName,
                                    DataName = reportAttribute.DataName,
                                    Alignment = reportAttribute.Alignment,
                                    Width = reportAttribute.Width,
                                    Append = reportAttribute.Append,
                                    Prepend = reportAttribute.Prepend
                                };
                                _context.CustomReportAttributes.Add(CustomReportAttributeobject);
                                _context.SaveChanges();
                            }
                        }
                    }
                    var existingreportcondition = _context.CustomReportConditions.Where(x => x.CustomReportId.ToString().ToLower().Equals(report.Id.ToString().ToLower())).ToList();
                    if (existingreportcondition != null && existingreportcondition.Count > 0)
                    {
                        foreach (var item in existingreportcondition)
                        {
                            _context.CustomReportConditions.Remove(item);
                            _context.SaveChanges();
                        }
                    }
                    if (report.CustomReportConditions != null && report.CustomReportConditions.Count > 0)
                    {
                        foreach (var reportcondition in report.CustomReportConditions)
                        {
                            var CustomReportConditionsobject = new CustomReportCondition()
                            {
                                CustomReportId = report.Id,
                                Condition = reportcondition.Condition,
                                Condition1 = reportcondition.Condition1,
                                Condition2 = reportcondition.Condition2,
                                Condition3 = reportcondition.Condition3,
                                Condition4 = reportcondition.Condition4
                            };
                            _context.CustomReportConditions.Add(CustomReportConditionsobject);
                            _context.SaveChanges();
                        }
                    }
                    ExistingReport.SortBy = report.SortBy;
                    ExistingReport.SortByAsc = report.SortByAsc;
                    ExistingReport.SortBy2 = report.SortBy2;
                    ExistingReport.SortBy2Asc = report.SortBy2Asc;
                    ExistingReport.DateFormat = report.DateFormat;
                    ExistingReport.ZeroCurrencyBlank = report.ZeroCurrencyBlank;
                    ExistingReport.HideCurrencySymbol = report.HideCurrencySymbol;
                    _context.CustomReports.Update(ExistingReport);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    var cus = report;
                    await _context.CustomReports.AddAsync(cus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
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
        //Edit
        public async Task<IActionResult> EditReport(string reportid)
        {
            var ExistingReport = _context.CustomReports.Where(x => x.Id.ToString().ToLower().Equals(reportid.ToLower())).FirstOrDefault();
            if (ExistingReport != null)
            {
                var reportGroup = GetReportProperties(ExistingReport.ReportGroup);
                //var selectedGroups = 
                return View("CreateReport", ExistingReport);
            }
            return RedirectToAction("Index");
        }
        public IActionResult GetReportPropertiesByReportId(string reportid)
        {
            try
            {
                var listofassestmodel = GetFilterForPage("asset_management");
                var listofPropertyModel = GetFilterForPage("property_management");
                var listofLicenseCertificateModel = GetFilterForPage("license_management");
                var listofPropertyAssesmentModel = GetFilterForPage("assesment_management");
                var listofTenantModel = GetFilterForPage("tenant_management");
                var allmodles = listofassestmodel.Where(x => x.CustomAttributes.Count() > 0 && x.CustomAttributes.Any(y => y.NamedArguments.Count > 0)).ToList();
                allmodles.AddRange(listofPropertyModel.Where(x => x.CustomAttributes.Count() > 0 && x.CustomAttributes.Any(y => y.NamedArguments.Count > 0)).ToList());
                allmodles.AddRange(listofLicenseCertificateModel.Where(x => x.CustomAttributes.Count() > 0 && x.CustomAttributes.Any(y => y.NamedArguments.Count > 0)).ToList());
                allmodles.AddRange(listofPropertyAssesmentModel.Where(x => x.CustomAttributes.Count() > 0 && x.CustomAttributes.Any(y => y.NamedArguments.Count > 0)).ToList());
                allmodles.AddRange(listofTenantModel.Where(x => x.CustomAttributes.Count() > 0 && x.CustomAttributes.Any(y => y.NamedArguments.Count > 0)).ToList());
                var listofselectedfields = _context.CustomReportAttributes.Where(x => x.CustomReportId.ToString().ToLower().Equals(reportid.ToLower())).ToList();
                var filterColumnsforEdit = new List<FilterColumnforEdit>();
                foreach (var col in listofselectedfields)
                {
                    var PropertyDetail = allmodles.Where(x => x.Name == col.DataName).FirstOrDefault();
                    var colType = Nullable.GetUnderlyingType(PropertyDetail.PropertyType) != null ?
                              Type.GetTypeCode(Nullable.GetUnderlyingType(PropertyDetail.PropertyType)).ToString() :
                              Type.GetTypeCode(PropertyDetail.PropertyType).ToString();
                    filterColumnsforEdit.Add(new FilterColumnforEdit()
                    {
                        Type = colType == "Object" ? "Guid" : colType,
                        Name = PropertyDetail.Name,
                        // In case of Guid, we have to see what to fetch in select dropdown
                        FetchType = colType == "Object" ? PropertyDetail.Name.Split("Id")[0] : null,
                        DisplayName = PropertyDetail.CustomAttributes.First(x => x.NamedArguments.Count > 0).NamedArguments.FirstOrDefault().TypedValue.Value?.ToString(),
                        Alignment = col.Alignment,
                        Width = col.Width,
                        Append = col.Append == null ? "" : col.Append,
                        Prepend = col.Prepend == null ? "" : col.Prepend
                    });
                }
                return Ok(filterColumnsforEdit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        public IActionResult GetReportConditionByReportId(string reportid)
        {
            try
            {
                var reportconditions = _context.CustomReportConditions.Where(x => x.CustomReportId.ToString().ToLower().Equals(reportid.ToLower())).ToList();
                return Ok(reportconditions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Delete
        public async Task<IActionResult> DeleteReport(string reportid)
        {
            var ExistingReport = _context.CustomReports.Where(x => x.Id.ToString().ToLower().Equals(reportid.ToLower())).FirstOrDefault();
            if (ExistingReport != null)
            {
                _context.CustomReports.Remove(ExistingReport);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //Duplicate
        public async Task<IActionResult> DuplicateReport(string reportid)
        {
            var ExistingReport = _context.CustomReports.Where(x => x.Id.ToString().ToLower().Equals(reportid.ToLower())).FirstOrDefault();
            if (ExistingReport != null)
            {
                ExistingReport.Id = new Guid();
                _context.CustomReports.AddAsync(ExistingReport);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //Set Favourity
        public async Task<IActionResult> FavouriteReport(string reportid)
        {
            var ExistingReport = _context.CustomReports.Where(x => x.Id.ToString().ToLower().Equals(reportid.ToLower())).FirstOrDefault();
            if (ExistingReport != null)
            {
                if (ExistingReport.Favourite == true)
                    ExistingReport.Favourite = false;
                else
                    ExistingReport.Favourite = true;
                _context.CustomReports.Update(ExistingReport);
                _context.SaveChanges();
            }
            return Json("Setted");
        }
    }
}
