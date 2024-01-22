namespace CromWood.Data.Entities
{
    public class UnitUtilityDocument : DBTable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DocUrl { get; set; }
        public Guid UnitUtilityId { get; set; }
        public UnitUtility UnitUtility { get; set; }
    }
}
