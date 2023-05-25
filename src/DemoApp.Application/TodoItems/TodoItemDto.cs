using DemoApp.Domain.Enums;

namespace DemoApp.Application.TodoItems
{
    public sealed class TodoItemDto
    {
        public int Id { get; init; }
        public int ListId { get; init; }

        public string? Title { get; init; }

        public string? Note { get; init; }

        public PriorityLevel Priority { get; init; }

        public DateTime? Reminder { get; init; }

        public bool Done { get; init; }
    }
}
