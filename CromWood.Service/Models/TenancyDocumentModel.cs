using Microsoft.AspNetCore.Http;

namespace CromWood.Business.Models
{
    public class TenancyDocumentModel
    {
        public Guid Id { get; set; }
        public string DocumentName { get; set; }
        public string DocUrl { get; set; }
        public Guid TenancyId { get; set; }
        public IFormFile File { get; set; }
    }
}
