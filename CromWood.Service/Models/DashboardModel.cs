using CromWood.Business.Models.ViewModel;

namespace CromWood.Business.Models
{
    public class DashboardModel
    {
        public float Earning { get; set; }
        public float Expenses { get; set; }
        public float PaidInvoices { get; set; }
        public float OpenInvoices { get; set; }
        public List<IncomeExpenseModel> IncomeExpenses { get; set; }
        public int Vaccant { get;set; }
        public int Occupied { get;set; }
        public int ExpiringSoon { get;set; }
        public List<TenancyViewModel> Tenancies { get; set; }
        public List<ComplaintViewModel> RecentComplaints { get; set; }
    }
}
