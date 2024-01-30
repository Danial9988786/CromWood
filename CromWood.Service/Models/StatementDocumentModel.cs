using Microsoft.AspNetCore.Http;

namespace CromWood.Business.Models
{
    public class StatementDocumentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DocUrl { get; set; }
        public IFormFile Document { get; set; }
        public Guid? StatementId { get; set; }
    }
}
