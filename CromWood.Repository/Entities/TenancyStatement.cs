using CromWood.Data.Entities.Default;

namespace CromWood.Data.Entities
{
    public class TenancyStatement: DBTable
    {
        public Guid Id { get; set; }
        public Guid StatementTypeId { get; set; }
        public StatementType StatementType { get; set; }
        public string ReferenceID { get; set; }
        public float NetAmount { get; set; }
        public DateTime Date { get; set; }
        public string StatementDescription { get; set; }
        public Guid TenancyId { get; set; }
        public Tenancy Tenancy { get; set; }
        public ICollection<StatementTransaction> Transactions { get; set; }
        public ICollection<StatementDocument> Documents { get; set; }

    }
}
