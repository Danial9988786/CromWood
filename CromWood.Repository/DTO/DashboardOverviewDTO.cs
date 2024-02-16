namespace CromWood.Data.DTO
{
    public class DashboardOverviewDTO
    {
        public float Earning { get; set; }
        public float Expenses { get; set; }
        public float PaidInvoices { get; set; }
        public float OpenInvoices { get; set; }
        public List<IncomeExpenseDTO> IncomeExpenses { get; set; }
        public int Vaccant { get; set; }
        public int Occupied { get; set; }
        public int ExpiringSoon { get; set; }
    }
}
