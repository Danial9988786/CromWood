

using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models.ViewModel
{
    public class StatementViewModel
    {
        public Guid Id { get; set; }
        public Guid StatementTypeId { get; set; }
        public StatementType StatementType { get; set; }
        public string ReferenceID { get; set; }
        public float NetAmount { get; set; }
        public DateTime Date { get; set; }
        public string StatementDescription { get; set; }
        public Guid TenancyId { get; set; }
        public TenancyViewModel Tenancy { get; set; }
        public ICollection<StatementTransactionViewModel> Transactions { get; set; }
        public ICollection<StatementDocumentViewModel> Documents { get; set; }
    }
}
