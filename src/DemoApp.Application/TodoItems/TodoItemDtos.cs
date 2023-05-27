using DemoApp.Domain.Enums;

namespace DemoApp.Application.TodoItems
{
    public sealed class TodoItemDto
    {
        public int Id { get; init; }
        public int TodoListId { get; init; }

        public string Title { get; init; } = string.Empty;

        public string? Note { get; init; }

        public PriorityLevel Priority { get; init; }

        public DateTime? Reminder { get; init; }

        public bool Done { get; init; }
    }

    public record TodoItemCreateDto
    {
        public int TodoListId { get; set; }

        public string Title { get; set; } = string.Empty;
    }
}
