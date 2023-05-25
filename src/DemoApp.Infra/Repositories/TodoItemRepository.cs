using DemoApp.Domain.Models;

namespace DemoApp.Infra.Repositories
{
    public interface ITodoItemRepository : IRepository<TodoItem> { }

    internal sealed class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(DemoAppContext context) : base(context) { }
    }
}
