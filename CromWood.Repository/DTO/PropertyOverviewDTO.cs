namespace CromWood.Data.DTO
{
    public class PropertyOverviewDTO
    {
        public int OccupiedDay { get; set; }
        public int VacantDay { get; set; }
        public List<IncomeExpenseDTO> IncomeExpenses { get; set; }
        public List<OccupiedVacantDTO> OccupiedVacants { get; set; }
    }

    public class IncomeExpenseDTO
    {
        public string Month { get; set; }
        public float Income { get; set; }
        public float Expense { get; set; }
    }
    public class OccupiedVacantDTO
    {
        public string Month { get; set; }
        public int OccupiedDays { get; set; }
    }
}
