using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class ComplaintModel
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid? ComplaintTypeId { get; set; }
        public Guid TenantId { get; set; }
        public DateTime? ExpectedResolveDate { get; set; }
        public Guid? ComplaintCategoryId { get; set; }
        public Guid? ComplaintNatureId { get; set; }

        [MaxLength(25)]
        public string Status { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        [MaxLength(100)]
        public string FileUrl { get; set; }

        // Status change things
        public string StatusUpdateRemark { get; set; }
        public IFormFile StatusUpdateFile { get; set; }
        public string StatusUpdateFileUrl { get; set; }
    }
}
