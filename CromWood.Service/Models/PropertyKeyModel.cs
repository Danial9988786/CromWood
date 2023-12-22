using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class PropertyKeyModel
    {
        public Guid Id { get; set; }
        public Guid PropertyKeyType { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int Quantity { get; set; }
        [MaxLength(250)]
        public string AdditionalInformation { get; set; }
        public bool? SharedWithTenant { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImageUrl { get; set; }
        public Guid PropertyId { get; set; }
    }
}
