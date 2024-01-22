namespace CromWood.Data.Entities
{
    public class Message:DBTable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        // Medium can be Email or Whatsapp.
        public string Medium { get; set; }

        // Schedule related fields
        public bool IsScheduled { get;set; }

        // OneTime, Daily, Weekly, Monthly, Anually
        public string ScheduleFrequency { get;set; }
        public int? ScheduledWeekDay { get;set; }
        public int? ScheduledMonthDay { get;set; }
        public int? ScheduledHour { get;set; }
        public int? ScheduledMinute { get;set; }
        public string AMPM { get;set; }
        public IEnumerable<MessageRecipient> Recipients { get; set; }
    }
}
