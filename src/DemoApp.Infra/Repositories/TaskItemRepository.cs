using DemoApp.Domain.Models;

namespace DemoApp.Infra.Repositories
{
    public interface ITaskItemRepository : IRepository<TaskItem> { }

    internal sealed class TaskItemRepository : Repository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(TaskManagerContext context) : base(context) { }
    }
}
