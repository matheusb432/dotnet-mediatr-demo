using Microsoft.EntityFrameworkCore;
using DemoApp.Domain.Models;

namespace DemoApp.Infra.Repositories
{
    public interface ITodoListRepository : IRepository<TodoList> { }

    internal sealed class TodoListRepository : Repository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(DemoAppContext context) : base(context) { }

        public override IQueryable<TodoList> Query()
        {
            return base.Query().Include(x => x.TodoItems);
        }
    }
}
