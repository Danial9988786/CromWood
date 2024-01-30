namespace CromWood.Data.Entities
{
    public class ChangeLog: DBTable
    {
        public Guid Id { get; set; }
        public string ScreenName { get; set; }
        public Guid ScreenDetailId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

    }
}
