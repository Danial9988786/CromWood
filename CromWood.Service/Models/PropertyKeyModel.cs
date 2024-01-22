using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class PropertyKeyModel : DBTable
    {
        public Guid Id { get; set; }
        public Guid PropertyKeyTypeId { get; set; }
        public PropertyKeyType PropertyKeyType { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int Quantity { get; set; }
        [MaxLength(250)]
        public string AdditionalInformation { get; set; }
        public bool SharedWithTenant { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImageUrl { get; set; }
        public Guid PropertyId { get; set; }
    }
}
