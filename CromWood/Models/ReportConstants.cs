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
        new FilterOption(){ Key= "Mon, Dec 1, 2023", Value ="ddd, MMM d yyyy"},
        new FilterOption(){ Key= "12 Dec 2023", Value ="dd MMM yyyy"},
        new FilterOption(){ Key= "2023 12 Dec", Value ="yyyy dd MMM"}
    };
    }
}
