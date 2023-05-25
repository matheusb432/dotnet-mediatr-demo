namespace DemoApp.Domain.Models
{
    public sealed class Timesheet : Entity
    {
        public Timesheet()
        {
            Tasks = new();
        }

        public DateTime Date { get; set; }
        public bool? Finished { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
}
