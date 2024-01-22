namespace CromWood.Data.Entities
{
    public class StatementDocument: DBTable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DocUrl { get; set; }
        public Guid? StatementId { get; set; }
        public TenancyStatement Statement { get; set; }
    }
}
