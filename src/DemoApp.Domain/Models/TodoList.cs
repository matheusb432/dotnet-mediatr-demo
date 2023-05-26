namespace DemoApp.Domain.Models
{
    public sealed class TodoList : Entity
    {
        public TodoList()
        {
            TodoItems = new List<TodoItem>();
        }

        public string Title { get; set; } = string.Empty;

        public List<TodoItem> TodoItems { get; set; }
    }
}
