namespace CromWood.Data.Entities
{
    public class CustomReport:DBTable
    {
        public Guid Id { get; set; }
        public string ReportGroup { get; set; }
        public bool Favourite { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CustomReportAttribute> CustomReportAttributes { get; set; }
        public List<CustomReportCondition> CustomReportConditions { get; set; }

        // Sorting Option
        public string SortBy { get;set; }
        public bool SortByAsc { get;set; }
        public string SortBy2 { get;set; }
        public bool SortBy2Asc { get;set; }

        // Additional Option
        public string DateFormat { get; set; }
        public bool ZeroCurrencyBlank { get; set; }
        public bool HideCurrencySymbol { get; set; }


    }
}
