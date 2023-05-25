namespace DemoApp.Domain.Models
{
    public sealed class TodoList : Entity
    {
        public string? Title { get; set; }

        public IList<TodoItem> Items { get; } = new List<TodoItem>();
    }
}
