using Microsoft.AspNetCore.Http;

namespace CromWood.Business.Models
{
    public class TenancyNoteModel
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public string FileUrl { get; set; }
        public bool ScheduleForLater { get; set; }
        public DateTime? ScheduleDateTime { get; set; }
        public Guid TenancyId { get; set; }
        public IFormFile File { get; set; }
    }
}
