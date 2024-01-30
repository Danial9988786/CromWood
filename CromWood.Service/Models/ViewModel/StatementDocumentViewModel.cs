namespace CromWood.Business.Models.ViewModel
{
    public class StatementDocumentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DocUrl { get; set; }
        public Guid? StatementId { get; set; }
        public StatementViewModel Statement { get; set; }
    }
}
