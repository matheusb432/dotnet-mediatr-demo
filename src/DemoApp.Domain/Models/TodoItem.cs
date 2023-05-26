using DemoApp.Domain.Enums;

namespace DemoApp.Domain.Models
{
    public sealed class TodoItem : Entity
    {
        public int TodoListId { get; set; }

        public string? Title { get; set; }

        public string? Note { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTime? Reminder { get; set; }

        public bool Done { get; set; }

        public TodoList TodoList { get; set; } = null!;
    }
}
