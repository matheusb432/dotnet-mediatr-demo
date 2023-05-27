using DemoApp.Application.TodoItems;
using DemoApp.Domain.Models;

namespace DemoApp.Application.TodoLists
{
    public sealed class TodoListDto
    {
        public TodoListDto()
        {
            TodoItems = new List<TodoItemDto>();
        }

        public string Title { get; set; } = string.Empty;

        public List<TodoItemDto> TodoItems { get; set; }
    }
}
