using CromWood.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace CromWood.Business.Models
{
    public class TenancyMessageModel:DBTable
    {
        public Guid Id { get; set; }
        public Guid TenancyId { get; set; }
        public string Medium { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public IFormFile Attachment { get; set; }
        public string AttachmentUrl { get; set; }
        public bool? IsDraft { get; set; }
    }
}
