namespace CromWood.Business.Models
{
    public class MessageModel
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
        public List<MessageRecipientModel> Recipients { get; set; }
        public IEnumerable<Guid> SelectedRecipients { get; set; }
    }

    public class MessageRecipientModel
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Guid RecipientId { get; set; }
    }
}
