using Microsoft.AspNetCore.Http;

namespace CromWood.Business.Models
{
    public class ComplaintCommentModel
    {
        public Guid Id { get; set; }
        public Guid ComplaintId { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string FileUrl { get; set; }
    }
}
