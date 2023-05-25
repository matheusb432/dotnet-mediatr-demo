using Microsoft.EntityFrameworkCore;
using DemoApp.Domain.Models;

namespace DemoApp.Infra.Repositories
{
    public interface ITodoListRepository : IRepository<TodoList> { }

    internal sealed class TodoListRepository : Repository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(DemoAppContext context) : base(context) { }
    }
}
