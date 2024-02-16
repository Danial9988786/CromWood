using CromWood.Data.Entities;

namespace CromWood.Business.Models.ViewModel
{
    public class MessageViewModel:DBTable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Medium { get; set; }
        public bool IsScheduled { get; set; }
        public string ScheduleFrequency { get; set; }
        public int? ScheduledWeekDay { get; set; }
        public int? ScheduledMonthDay { get; set; }
        public int? ScheduledHour { get; set; }
        public int? ScheduledMinute { get; set; }
        public string AMPM { get; set; }
        public IEnumerable<MessageRecipientViewModel> Recipients { get; set; }
    }

    public class MessageRecipientViewModel
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Guid RecipientId { get; set; }
        public TenantViewModel Recipient { get; set; }
    }
}
