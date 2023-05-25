namespace DemoApp.Domain.Models
{
    public sealed class TaskItem : Entity
    {
        public string Title { get; set; } = string.Empty;
        public short? Time { get; set; }
        public short? Rating { get; set; }
        public short Importance { get; set; }
        public string? Comment { get; set; }
        public int TimesheetId { get; set; }
        public Timesheet? Timesheet { get; set; }
    }
}
