using CromWood.Models;

namespace CromWood.Business
{
    public static class ReportConstants
    {
        public static List<FilterOption> ReportGroups = new()
    {
        new FilterOption(){ Key= "Asset Managment", Value ="asset_management"},
        new FilterOption(){ Key= "Property Management", Value ="property_management"},
        new FilterOption(){ Key= "Tenancy Management", Value ="tenancy_management"}
    };

        public static List<FilterOption> ReportDateFormat = new()
    {
        new FilterOption(){ Key= ""+DateTime.Now.DayOfWeek.ToString().Substring(0,3)+", "+DateTime.Now.ToString("MMM")+" "+DateTime.Now.Day+", "+DateTime.Now.Year+"", Value ="ddd, MMM d yyyy"},
        new FilterOption(){ Key= ""+DateTime.Now.Day+" "+DateTime.Now.ToString("MMM")+" "+DateTime.Now.Year+"", Value ="dd MMM yyyy"},
        new FilterOption(){ Key= ""+DateTime.Now.Year+" "+DateTime.Now.Day+" "+DateTime.Now.ToString("MMM")+"", Value ="yyyy dd MMM"}
    };
    }
}
