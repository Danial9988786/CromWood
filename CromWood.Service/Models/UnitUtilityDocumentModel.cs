using Microsoft.AspNetCore.Http;

namespace CromWood.Business.Models
{
    public class UnitUtilityDocumentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DocUrl { get; set; }
        public Guid UnitUtilityId { get; set; }
        public IFormFile Document { get; set; }
    }
}
