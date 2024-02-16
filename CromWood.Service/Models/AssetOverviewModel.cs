namespace CromWood.Business.Models
{
    public class AssetOverviewModel
    {
        public float ExpectedEarning { get; set; }
        public float Earning { get; set; }
        public float Expenses { get; set; }
        public float TotalProfit { get; set; }
        public List<AssetOverviewPropertyDetail> Properties { get; set; }
        public List<AssetOverviewPropertyDetail> TopPerformingProperties { get; set; }
    }

    public class AssetOverviewPropertyDetail
    {

        public Guid Id { get; set; }
        public string PropertyID { get; set; }
        public string Status { get; set; }
        public string TenantName { get; set; }
        public DateTime? TenancyStartDate { get; set; }
        public int TenancyDuration { get; set; }
        public DateTime? TenancyEndDate { get; set; }
        public string OccupiedByVacantDays { get; set; }
        public float ExpectedEarning { get; set; }
        public float ActualEarning { get; set; }
    }
}
