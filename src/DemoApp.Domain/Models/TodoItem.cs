using DemoApp.Domain.Enums;

namespace DemoApp.Domain.Models
{
    public sealed class TodoItem : Entity
    {
        public int ListId { get; set; }

        public string? Title { get; set; }

        public string? Note { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTime? Reminder { get; set; }

        public bool Done { get; set; }

        public TodoList List { get; set; } = null!;
    }
}
