using CromWood.Business.Models.ViewModel;

namespace CromWood.Business.Models
{
    public class PropertyOverviewModel
    {
        public Guid Id { get;set; }
        public int OccupiedDay { get; set; }
        public int VacantDay { get; set; }
        public List<IncomeExpenseModel> IncomeExpenses { get; set; }
        public List<OccupiedVacantModel> OccupiedVacants { get; set; }
        public List<StatementViewModel> RecentTransactions { get; set; }
        public List<ComplaintViewModel> RecentComplaints { get; set; }
    }

    public class IncomeExpenseModel
    {
        public string Month { get; set; }
        public float Income { get; set; }
        public float Expense { get; set; }
    }
    public class OccupiedVacantModel
    {
        public string Month { get; set; }
        public int OccupiedDays { get; set; }
    }
}
